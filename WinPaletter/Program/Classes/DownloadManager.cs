using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WinPaletter
{
    /// <summary>
    /// Download manager for WinPaletter
    /// </summary>
    public class DownloadManager : IDisposable
    {
        public DownloadManager()
        {
            client = CreateHttpClient();
        }

        #region Fields

        private readonly object lockObject = new();
        private CancellationTokenSource cancellationTokenSource;
        private readonly HttpClient client = new();

        #endregion

        #region Events

        /// <summary>
        /// Event raised when the download progress changes
        /// </summary>
        public event EventHandler<DownloadProgressEventArgs> DownloadProgressChanged;

        /// <summary>
        /// Event raised when the download is completed
        /// </summary>
        public event EventHandler<System.ComponentModel.AsyncCompletedEventArgs> DownloadFileCompleted;

        /// <summary>       
        /// Event raised when an error occurs during the download
        /// </summary>
        public event EventHandler<string> DownloadErrorOccurred;

        #endregion

        #region Properties

        /// <summary>
        /// Gets whether the download manager is busy downloading a file
        /// </summary>
        public bool IsBusy { get; private set; }

        #endregion

        #region Async

        /// <summary>
        /// Read string from a URL asynchronously
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> ReadStringAsync(string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Download a file from URL  asynchronously
        /// </summary>
        /// <param name="url"></param>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        public async Task DownloadFileAsync(string url, string destinationPath)
        {
            IsBusy = true;
            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                long totalBytes = await GetFileSizeFromUrlAsync(url);

                HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationTokenSource.Token);
                response.EnsureSuccessStatusCode();

                using (Stream stream = await response.Content.ReadAsStreamAsync())
                using (FileStream fileStream = System.IO.File.Create(destinationPath))
                {
                    byte[] buffer = new byte[8192];
                    int bytesRead;
                    long totalBytesRead = 0;

                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationTokenSource.Token)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                        totalBytesRead += bytesRead;

                        OnDownloadProgressChanged(new DownloadProgressEventArgs(totalBytesRead, totalBytes));

                        if (cancellationTokenSource.Token.IsCancellationRequested)
                            break;
                    }
                }

                OnDownloadCompleted(new(null, false, new object()));
            }
            catch (Exception ex)
            {
                IsBusy = false;
                OnDownloadErrorOccurred($"Error downloading file: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>   
        /// Get file size from URL asynchronously
        /// </summary>
        public async Task<long> GetFileSizeFromUrlAsync(string url)
        {
            try
            {
                using (HttpResponseMessage response = await client.SendAsync(new(HttpMethod.Head, url)))
                {
                    response.EnsureSuccessStatusCode();
                    return response.Content.Headers.ContentLength.GetValueOrDefault();
                }
            }
            catch (Exception ex)
            {
                OnDownloadErrorOccurred($"Error getting file size from URL: {ex.Message}");
                return 0;
            }
        }

        #endregion

        #region Public Methods

        private HttpClient CreateHttpClient()
        {
            HttpClientHandler handler = new()
            {
                UseDefaultCredentials = true,
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,
                SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls11 | System.Security.Authentication.SslProtocols.Tls

            };

            return new HttpClient(handler)
            {
                Timeout = TimeSpan.FromMilliseconds(Program.Timeout)
            };
        }

        /// <summary>
        /// Read string from a URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        public string ReadString(string url)
        {
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Download a file from URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="destinationPath"></param>
        public void DownloadFile(string url, string destinationPath)
        {
            IsBusy = true;
            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                long totalBytes = GetFileSizeFromUrl(url);

                HttpResponseMessage response = client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationTokenSource.Token).Result;
                response.EnsureSuccessStatusCode();

                using (Stream stream = response.Content.ReadAsStreamAsync().Result)
                using (FileStream fileStream = System.IO.File.Create(destinationPath))
                {
                    byte[] buffer = new byte[8192];
                    int bytesRead;
                    long totalBytesRead = 0;

                    while ((bytesRead = stream.ReadAsync(buffer, 0, buffer.Length, cancellationTokenSource.Token).Result) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                        totalBytesRead += bytesRead;

                        OnDownloadProgressChanged(new DownloadProgressEventArgs(totalBytesRead, totalBytes));

                        if (cancellationTokenSource.Token.IsCancellationRequested)
                            break;
                    }
                }

                OnDownloadCompleted(new(null, false, new object()));
            }
            catch (Exception ex)
            {
                IsBusy = false;
                OnDownloadErrorOccurred($"Error downloading file: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Get file size from URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public long GetFileSizeFromUrl(string url)
        {
            try
            {
                using (HttpResponseMessage response = client.SendAsync(new(HttpMethod.Head, url)).Result)
                {
                    response.EnsureSuccessStatusCode();
                    return response.Content.Headers.ContentLength.GetValueOrDefault();
                }
            }
            catch (Exception ex)
            {
                OnDownloadErrorOccurred($"Error getting file size from URL: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Pause the currently running download
        /// </summary>
        public void PauseDownload()
        {
            lock (lockObject)
            {
                cancellationTokenSource?.Cancel();
            }
        }

        /// <summary>
        /// Stop the currently running download
        /// </summary>
        public void StopDownload()
        {
            lock (lockObject)
            {
                cancellationTokenSource?.Cancel();
                OnDownloadCompleted(new(null, true, new object()));
                // Additional cleanup if needed
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Method to raise the DownloadProgressChanged event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDownloadProgressChanged(DownloadProgressEventArgs e)
        {
            DownloadProgressChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Method to raise the DownloadFileCompleted event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDownloadCompleted(System.ComponentModel.AsyncCompletedEventArgs e)
        {
            DownloadFileCompleted?.Invoke(this, e);
        }

        /// <summary>
        /// Method to raise the DownloadErrorOccurred event
        /// </summary>
        /// <param name="errorMessage"></param>
        protected virtual void OnDownloadErrorOccurred(string errorMessage)
        {
            DownloadErrorOccurred?.Invoke(this, errorMessage);
        }

        #endregion

        #region Classes

        /// <summary>
        /// Event arguments for the DownloadProgressChanged event
        /// </summary>
        public class DownloadProgressEventArgs : EventArgs
        {
            /// <summary>
            /// Bytes received so far by download manager
            /// </summary>
            public long BytesReceived { get; }

            /// <summary>
            /// Total bytes to receive. It could be zero if the server doesn't return the content length.
            /// </summary>
            public long TotalBytesToReceive { get; }

            public long ProgressPercentage => TotalBytesToReceive > 0 ? BytesReceived * 100 / TotalBytesToReceive : 0;

            /// <summary>
            /// Constructor for DownloadProgressEventArgs
            /// </summary>
            /// <param name="bytesDownloaded"></param>
            /// <param name="totalBytes"></param>
            public DownloadProgressEventArgs(long bytesDownloaded, long totalBytes)
            {
                BytesReceived = bytesDownloaded;
                TotalBytesToReceive = totalBytes;
            }
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Dispose the download manager
        /// </summary>
        public void Dispose()
        {
            StopDownload();
            client?.Dispose();
            cancellationTokenSource?.Dispose();
        }

        #endregion
    }
}
