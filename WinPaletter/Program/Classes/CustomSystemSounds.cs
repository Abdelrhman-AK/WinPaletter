using System.IO;
using WinPaletter;

namespace System.Media
{
    /// <summary>
    /// Provides access to common system sounds and custom sound overrides for Windows program events.
    /// </summary>
    /// <remarks>This class exposes static properties representing standard Windows sounds, such as Asterisk,
    /// Beep, Exclamation, Hand, Question, Navigation, and Close. Each property returns a <see
    /// cref="CustomSystemSounds.Sound"/> instance that can play either the current Windows sound scheme's sound for the
    /// event or a custom sound file if configured. Use the <see cref="CustomSystemSounds.Sound.Play"/> method to play
    /// the associated sound. This class cannot be instantiated.</remarks>
    public sealed class CustomSystemSounds
    {
        /// <summary>
        /// Gets the sound associated with the Asterisk program event in the current Windows sound scheme,
        /// or a custom path defined in <c>Program.TM.Sounds.Snd_Win_SystemAsterisk</c>.
        /// </summary>
        public static Sound Asterisk { get; } = new(64, () => Program.TM.Sounds.Snd_Win_SystemAsterisk);

        /// <summary>
        /// Gets the default beep sound in Windows, or a custom path defined in <c>Program.TM.Sounds.Snd_Win_Default</c>.
        /// </summary>
        public static Sound Beep { get; } = new(0, () => Program.TM.Sounds.Snd_Win_Default);

        /// <summary>
        /// Gets the sound associated with the Exclamation program event in the current Windows sound scheme,
        /// or a custom path defined in <c>Program.TM.Sounds.Snd_Win_SystemExclamation</c>.
        /// </summary>
        public static Sound Exclamation { get; } = new(48, () => Program.TM.Sounds.Snd_Win_SystemExclamation);

        /// <summary>
        /// Gets the sound associated with the Hand program event in the current Windows sound scheme,
        /// or a custom path defined in <c>Program.TM.Sounds.Snd_Win_SystemHand</c>.
        /// </summary>
        public static Sound Hand { get; } = new(16, () => Program.TM.Sounds.Snd_Win_SystemHand);

        /// <summary>
        /// Gets the sound associated with the Question program event in the current Windows sound scheme,
        /// or a custom path defined in <c>Program.TM.Sounds.Snd_Win_SystemQuestion</c>.
        /// </summary>
        public static Sound Question { get; } = new(32, () => Program.TM.Sounds.Snd_Win_SystemQuestion);

        /// <summary>
        /// Gets the navigation sound for Windows Explorer operations,
        /// or a custom path defined in <c>Program.TM.Sounds.Snd_Explorer_Navigating</c>.
        /// </summary>
        public static Sound Navigation { get; } = new(100, () => Program.TM.Sounds.Snd_Explorer_Navigating);

        /// <summary>
        /// Gets the close window sound, or a custom path defined in <c>Program.TM.Sounds.Snd_Win_Close</c>.
        /// </summary>
        public static Sound Close { get; } = new(103, () => Program.TM.Sounds.Snd_Win_Close);


        private CustomSystemSounds() { }

        public sealed class Sound
        {
            private readonly int _id;
            private SoundPlayer _player;
            private bool _altPlayingMethod;
            private readonly Func<string> _getPath;

            internal Sound(int id, Func<string> getPath)
            {
                _id = id;
                _getPath = getPath;
            }

            public void Play()
            {
                try
                {
                    _altPlayingMethod = false;
                    string path = _getPath?.Invoke();

                    if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    {
                        _player?.Stop();
                        _player?.Dispose();

                        try
                        {
                            using FileStream fs = new(path, FileMode.Open, FileAccess.Read);
                            _player = new(fs);
                            return;
                        }
                        catch
                        {
                            _altPlayingMethod = true;
                            WinPaletter.NativeMethods.Helpers.StopAudio();
                            WinPaletter.NativeMethods.Helpers.PlayAudio(path);
                            return;
                        }
                    }
                }
                catch { } // Suppress any exceptions if there are no drivers installed or other issues
            }
        }
    }
}
