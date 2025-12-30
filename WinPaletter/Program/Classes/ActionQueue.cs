using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WinPaletter
{
    public class ActionQueue
    {
        public enum ActionProgressType { Started, Completed, Cancelled, Exception, TaskProgress }

        public class QueuedAction
        {
            public Func<CancellationToken, IProgress<int>, Task> Action { get; }
            public string Description { get; }
            public DateTime AddedTime { get; }
            internal CancellationTokenSource Cancellation { get; }
            internal ManualResetEventSlim PauseEvent { get; }

            public QueuedAction(Func<CancellationToken, IProgress<int>, Task> action, string description)
            {
                Action = action;
                Description = description;
                AddedTime = DateTime.Now;
                Cancellation = new CancellationTokenSource();
                PauseEvent = new ManualResetEventSlim(true);
            }

            public void Pause() => PauseEvent.Reset();
            public void Resume() => PauseEvent.Set();
            public void Cancel() => Cancellation.Cancel();
        }

        public class ActionProgressEventArgs : EventArgs
        {
            public QueuedAction ActionData { get; }
            public int CurrentIndex { get; }
            public int TotalTasks { get; }
            public ActionProgressType ProgressType { get; }
            public int? TaskPercent { get; }
            public DateTime EventTime { get; }

            public ActionProgressEventArgs(QueuedAction actionData, int currentIndex, int totalTasks, ActionProgressType type, int? percent = null)
            {
                ActionData = actionData;
                CurrentIndex = currentIndex;
                TotalTasks = totalTasks;
                ProgressType = type;
                TaskPercent = percent;
                EventTime = DateTime.Now;
            }
        }

        public class ActionEventArgs : EventArgs
        {
            public QueuedAction ActionData { get; }
            public DateTime EventTime { get; }

            public ActionEventArgs(QueuedAction actionData)
            {
                ActionData = actionData;
                EventTime = DateTime.Now;
            }
        }

        private readonly Queue<QueuedAction> _queue = new();
        private int _totalTasksEverAdded = 0;
        private bool _isRunning = false;
        private bool _queuePaused = false;
        private readonly object _lock = new();

        // Events
        public event EventHandler<ActionProgressEventArgs>? OnActionProgress;
        public event EventHandler<ActionEventArgs>? OnQueuePaused;
        public event EventHandler<ActionEventArgs>? OnQueueResumed;
        public event EventHandler<ActionEventArgs>? OnAllActionsCompleted;

        public bool IsRunning { get { lock (_lock) return _isRunning; } }
        public bool IsPaused { get { lock (_lock) return _queuePaused; } }

        // Enqueue sync
        public void Enqueue(Action action, string description = "")
        {
            Enqueue((ct, progress) =>
            {
                action();
                return Task.CompletedTask;
            }, description);
        }

        // Enqueue async with progress
        public void Enqueue(Func<CancellationToken, IProgress<int>, Task> action, string description = "")
        {
            var queuedAction = new QueuedAction(action, description);
            lock (_lock)
            {
                _queue.Enqueue(queuedAction);
                _totalTasksEverAdded++;
                if (!_isRunning && !_queuePaused)
                    _ = ProcessQueueAsync();
            }
        }

        private async Task ProcessQueueAsync()
        {
            while (true)
            {
                QueuedAction? queuedAction;
                int currentIndex;
                int totalTasks;

                lock (_lock)
                {
                    if (_queue.Count == 0)
                    {
                        _isRunning = false;
                        OnAllActionsCompleted?.Invoke(this, null!);
                        return;
                    }

                    if (_queuePaused) return;

                    _isRunning = true;
                    queuedAction = _queue.Dequeue();
                    currentIndex = _totalTasksEverAdded - _queue.Count;
                    totalTasks = _totalTasksEverAdded;
                }

                OnActionProgress?.Invoke(this, new ActionProgressEventArgs(queuedAction, currentIndex, totalTasks, ActionProgressType.Started));

                try
                {
                    queuedAction.PauseEvent.Wait(queuedAction.Cancellation.Token);

                    // Provide progress callback to task
                    Progress<int> progressReporter = new(p =>
                    {
                        OnActionProgress?.Invoke(this, new ActionProgressEventArgs(queuedAction, currentIndex, totalTasks, ActionProgressType.TaskProgress, p));
                    });

                    await queuedAction.Action(queuedAction.Cancellation.Token, progressReporter);

                    if (!queuedAction.Cancellation.IsCancellationRequested)
                        OnActionProgress?.Invoke(this, new ActionProgressEventArgs(queuedAction, currentIndex, totalTasks, ActionProgressType.Completed));
                }
                catch (OperationCanceledException)
                {
                    OnActionProgress?.Invoke(this, new ActionProgressEventArgs(queuedAction, currentIndex, totalTasks, ActionProgressType.Cancelled));
                }
                catch (Exception ex)
                {
                    OnActionProgress?.Invoke(this, new ActionProgressEventArgs(queuedAction, currentIndex, totalTasks, ActionProgressType.Exception));
                }
            }
        }

        public void PauseQueue() { lock (_lock) { _queuePaused = true; OnQueuePaused?.Invoke(this, null!); } }
        public void ResumeQueue() { lock (_lock) { _queuePaused = false; OnQueueResumed?.Invoke(this, null!); if (!_isRunning) _ = ProcessQueueAsync(); } }

        public void PauseTask(QueuedAction task) => task.Pause();
        public void ResumeTask(QueuedAction task) => task.Resume();
        public bool CancelTask(QueuedAction task)
        {
            lock (_lock)
            {
                if (_queue.Contains(task)) { task.Cancel(); _queue.Remove(a => a == task); return true; }
                else { task.Cancel(); return false; }
            }
        }

        public void Clear() { lock (_lock) { foreach (var t in _queue) t.Cancel(); _queue.Clear(); } }
        public List<(string Description, DateTime AddedTime)> GetPending() { lock (_lock) return _queue.Select(a => (a.Description, a.AddedTime)).ToList(); }
    }

    public static class QueueExtensions
    {
        public static bool Remove<T>(this Queue<T> queue, Predicate<T> match)
        {
            bool removed = false;
            Queue<T> temp = new();
            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                if (!removed && match(item)) { removed = true; continue; }
                temp.Enqueue(item);
            }
            while (temp.Count > 0) queue.Enqueue(temp.Dequeue());
            return removed;
        }
    }
}