using Serilog.Events;
using System;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace WinPaletter
{
    /// <summary>
    /// Download manager for WinPaletter
    /// </summary>
    public class DownloadManager : IDisposable
    {
        /// <summary>
        /// Initialize a new instance of <see cref="DownloadManager"/>
        /// </summary>
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
        public event EventHandler<AsyncCompletedEventArgs> DownloadFileCompleted;

        /// <summary>       
        /// Event raised when an error occurs during the download
        /// </summary>
        public event EventHandler<string> DownloadErrorOccurred;

        #endregion

        #region Properties

        /// <summary>
        /// Gets whether the download manager is busy downloading a File
        /// </summary>
        public bool IsBusy { get; private set; }

        #endregion

        #region Async

        /// <summary>
        /// Read string from a URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<string> ReadStringAsync(string url)
        {
            if (url.Contains("github.com"))
            {
                url = url.Replace("github.com", "raw.githubusercontent.com");
                url = url.Replace("/tree/", "/");
                url = url.Replace("/blob/", "/");
                url = url.Replace("?raw=true", string.Empty);
            }

            string result;

            using (HttpResponseMessage response = client.GetAsync(url).Result)
            {
                if (!response.IsSuccessStatusCode)
                {
                    Program.Log?.Write(LogEventLevel.Error, $"Couldn't read string from `{url}`");
                    throw new HttpRequestException($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
                else
                {
                    result = await response.Content.ReadAsStringAsync();
                    Program.Log?.Write(LogEventLevel.Information, $"Reading string from URL `{url}` returned `{result}`");
                }
            }

            return result;
        }

        /// <summary>
        /// Download a File from URL asynchronously
        /// </summary>
        /// <param name="url"></param>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        public async Task DownloadFileAsync(string url, string destinationPath)
        {
            if (url.Contains("github.com"))
            {
                url = url.Replace("github.com", "raw.githubusercontent.com");
                url = url.Replace("/tree/", "/");
                url = url.Replace("/blob/", "/");
                url = url.Replace("?raw=true", string.Empty);
            }

            // Set the IsBusy flag to true
            IsBusy = true;

            // Create a new CancellationTokenSource
            cancellationTokenSource = new();

            try
            {
                // Send a GET request to the URL and get the response
                using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationTokenSource.Token))
                {
                    // Ensure the response is successful
                    response.EnsureSuccessStatusCode();

                    // Get the total file size from the Content-Length header
                    long totalBytes = response.Content.Headers.ContentLength.GetValueOrDefault();

                    // Open a stream to read the response content and create a file stream to write the downloaded data
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    using (FileStream fileStream = File.Create(destinationPath))
                    {
                        // Create a buffer to read the data
                        byte[] buffer = new byte[8192];

                        // Read the data and write it to the file stream
                        int bytesRead;

                        // Variable to keep track of the total bytes read
                        long totalBytesRead = 0;

                        // Read the data in chunks and write it to the file stream
                        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationTokenSource.Token)) > 0)
                        {
                            // Write the downloaded data
                            fileStream.Write(buffer, 0, bytesRead);
                            totalBytesRead += bytesRead;

                            // Raise the DownloadProgressChanged event
                            OnDownloadProgressChanged(new DownloadProgressEventArgs(totalBytesRead, totalBytes));

                            // Check if the download is cancelled
                            if (cancellationTokenSource.Token.IsCancellationRequested) break;
                        }
                    }
                }

                // Raise the DownloadCompleted event when the download is completed
                OnDownloadCompleted(new(null, false, null));
            }
            catch (Exception ex)
            {
                // Raise the DownloadErrorOccurred event if an error occurs
                IsBusy = false;

                Program.Log?.Write(LogEventLevel.Error, $"Couldn't download `{url}` as `{destinationPath}`", ex);
                OnDownloadErrorOccurred($"Error downloading file: {ex.Message}", ex);
            }
            finally
            {
                // Set the IsBusy flag to false when the download is completed
                Program.Log?.Write(LogEventLevel.Information, $"File from URL `{url}` is saved as `{destinationPath}`");
                IsBusy = false;
            }
        }

        /// <summary>
        /// Download a part of a file from URL asynchronously
        /// </summary>
        /// <param name="url"></param>
        /// <param name="destinationPath"></param>
        /// <param name="startByte"></param>
        /// <param name="endByte"></param>
        /// <returns></returns>
        public async Task DownloadFilePartAsync(string url, string destinationPath, long startByte, long endByte)
        {
            if (url.Contains("github.com"))
            {
                url = url.Replace("github.com", "raw.githubusercontent.com");
                url = url.Replace("/tree/", "/");
                url = url.Replace("/blob/", "/");
                url = url.Replace("?raw=true", string.Empty);
            }

            // Set the IsBusy flag to true
            IsBusy = true;

            // Create a new CancellationTokenSource
            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                // Send a GET request to the URL with the Range header to download a specific part of the file
                HttpRequestMessage request = new(HttpMethod.Get, url)
                {
                    Headers =
            {
                Range = new RangeHeaderValue(startByte, endByte)
            }
                };

                HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationTokenSource.Token);

                // Ensure the response is successful
                response.EnsureSuccessStatusCode();

                // Open a stream to read the response content and create a file stream to write the downloaded data
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                using (FileStream fileStream = File.Create(destinationPath))
                {
                    // Create a buffer to read the data
                    byte[] buffer = new byte[8192];

                    // Read the data and write it to the file stream
                    int bytesRead;
                    long totalBytesRead = 0;

                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationTokenSource.Token)) > 0)
                    {
                        // Write the downloaded data
                        fileStream.Write(buffer, 0, bytesRead);
                        totalBytesRead += bytesRead;

                        // Raise the DownloadProgressChanged event when the download progress changes
                        OnDownloadProgressChanged(new DownloadProgressEventArgs(totalBytesRead, endByte - startByte + 1));

                        // Check if the download is cancelled
                        if (cancellationTokenSource.Token.IsCancellationRequested)
                            break;
                    }
                }

                // Raise the DownloadCompleted event when the download is completed
                OnDownloadCompleted(new(null, false, null));
            }
            catch (Exception ex)
            {
                // Raise the DownloadErrorOccurred event if an error occurs
                IsBusy = false;
                Program.Log?.Write(LogEventLevel.Error, $"Couldn't download `{url}` as `{destinationPath}` from byte {startByte} for length of {endByte} bytes", ex);
                OnDownloadErrorOccurred($"Error downloading file from byte {startByte} for length of {endByte} bytes: {ex.Message}", ex);
            }
            finally
            {
                // Set the IsBusy flag to false when the download is completed
                Program.Log?.Write(LogEventLevel.Information, $"File from URL `{url}` is saved as `{destinationPath}` from byte {startByte} for length of {endByte} bytes");
                IsBusy = false;
            }
        }

        /// <summary>   
        /// Get File size from URL asynchronously
        /// </summary>
        public async Task<long> GetFileSizeFromUrlAsync(string url)
        {
            if (url.Contains("github.com"))
            {
                url = url.Replace("github.com", "raw.githubusercontent.com");
                url = url.Replace("/tree/", "/");
                url = url.Replace("/blob/", "/");
                url = url.Replace("?raw=true", string.Empty);
            }

            long result = 0;

            try
            {
                using (HttpResponseMessage response = await client.SendAsync(new(HttpMethod.Head, url)))
                {
                    response.EnsureSuccessStatusCode();

                    result = response.Content.Headers.ContentLength.GetValueOrDefault();
                }

                Program.Log?.Write(LogEventLevel.Information, $"File size from URL `{url}` is `{result}` bytes");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Couldn't get file size from `{url}`", ex);
                OnDownloadErrorOccurred($"Error getting file size from URL: {ex.Message}", ex);
                result = 0;
            }

            return result;
        }

        #endregion

        #region Public Methods

        private HttpClient CreateHttpClient()
        {
            HttpClientHandler handler = new()
            {
                UseDefaultCredentials = true,
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,
                SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls

            };

            Program.Log?.Write(LogEventLevel.Information, $"Creating HttpClient with timeout of {Program.Timeout} ms and protocols {handler.SslProtocols}");

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
            if (url.Contains("github.com"))
            {
                url = url.Replace("github.com", "raw.githubusercontent.com");
                url = url.Replace("/tree/", "/");
                url = url.Replace("/blob/", "/");
                url = url.Replace("?raw=true", string.Empty);
            }

            string result;

            using (HttpResponseMessage response = client.GetAsync(url).Result)
            {
                if (!response.IsSuccessStatusCode)
                {
                    Program.Log?.Write(LogEventLevel.Error, $"Couldn't read string from `{url}`");
                    throw new HttpRequestException($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
                else
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    Program.Log?.Write(LogEventLevel.Information, $"Reading string from URL `{url}` returned `{result}`");
                }
            }

            return result;
        }

        /// <summary>
        /// Download a File from URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="destinationPath"></param>
        public void DownloadFile(string url, string destinationPath)
        {
            if (url.Contains("github.com"))
            {
                url = url.Replace("github.com", "raw.githubusercontent.com");
                url = url.Replace("/tree/", "/");
                url = url.Replace("/blob/", "/");
                url = url.Replace("?raw=true", string.Empty);
            }

            // Set the IsBusy flag to true
            IsBusy = true;

            // Create a new CancellationTokenSource
            cancellationTokenSource = new();

            try
            {
                // Send a GET request to the URL and get the response
                using (HttpResponseMessage response = client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationTokenSource.Token).Result)
                {
                    // Ensure the response is successful
                    response.EnsureSuccessStatusCode();

                    // Get the total file size
                    long totalBytes = response.Content.Headers.ContentLength.GetValueOrDefault();

                    // Open a stream to read the response content and create a file stream to write the downloaded data
                    using (Stream stream = response.Content.ReadAsStreamAsync().Result)
                    using (FileStream fileStream = File.Create(destinationPath))
                    {
                        // Create a buffer to read the data
                        byte[] buffer = new byte[8192];

                        // Read the data and write it to the file stream
                        int bytesRead;
                        long totalBytesRead = 0;

                        while ((bytesRead = stream.ReadAsync(buffer, 0, buffer.Length, cancellationTokenSource.Token).Result) > 0)
                        {
                            // Write the downloaded data
                            fileStream.Write(buffer, 0, bytesRead);
                            totalBytesRead += bytesRead;

                            // Raise the DownloadProgressChanged event when the download progress changes
                            OnDownloadProgressChanged(new DownloadProgressEventArgs(totalBytesRead, totalBytes));

                            // Check if the download is cancelled
                            if (cancellationTokenSource.Token.IsCancellationRequested)
                                break;
                        }
                    }
                }
                // Raise the DownloadCompleted event when the download is completed
                IsBusy = false;
                OnDownloadCompleted(new(null, false, new object()));
            }
            catch (Exception ex)
            {
                // Raise the DownloadErrorOccurred event if an error occurs
                IsBusy = false;
                Program.Log?.Write(LogEventLevel.Error, $"Couldn't download `{url}` as `{destinationPath}`", ex);
                OnDownloadErrorOccurred($"Error downloading file: {ex.Message}", ex);
            }
            finally
            {
                // Set the IsBusy flag to false when the download is completed
                Program.Log?.Write(LogEventLevel.Information, $"File from URL `{url}` is saved as `{destinationPath}`");
                IsBusy = false;
            }
        }

        /// <summary>
        /// Download a part of a file from URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="destinationPath"></param>
        /// <param name="startByte"></param>
        /// <param name="endByte"></param>
        public void DownloadFilePart(string url, string destinationPath, long startByte, long endByte)
        {
            if (url.Contains("github.com"))
            {
                url = url.Replace("github.com", "raw.githubusercontent.com");
                url = url.Replace("/tree/", "/");
                url = url.Replace("/blob/", "/");
                url = url.Replace("?raw=true", string.Empty);
            }

            // Set the IsBusy flag to true
            IsBusy = true;

            // Create a new CancellationTokenSource
            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                // Send a GET request to the URL with the Range header to download a specific part of the file
                HttpRequestMessage request = new(HttpMethod.Get, url)
                {
                    Headers =
            {
                Range = new RangeHeaderValue(startByte, endByte)
            }
                };

                HttpResponseMessage response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationTokenSource.Token).Result;

                // Ensure the response is successful
                response.EnsureSuccessStatusCode();

                // Open a stream to read the response content and create a file stream to write the downloaded data
                using (Stream stream = response.Content.ReadAsStreamAsync().Result)
                using (FileStream fileStream = File.Create(destinationPath))
                {
                    // Create a buffer to read the data
                    byte[] buffer = new byte[8192];

                    // Read the data and write it to the file stream
                    int bytesRead;
                    long totalBytesRead = 0;

                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        // Write the downloaded data
                        fileStream.Write(buffer, 0, bytesRead);
                        totalBytesRead += bytesRead;

                        // Raise the DownloadProgressChanged event when the download progress changes
                        OnDownloadProgressChanged(new DownloadProgressEventArgs(totalBytesRead, endByte - startByte + 1));

                        // Check if the download is cancelled
                        if (cancellationTokenSource.Token.IsCancellationRequested)
                            break;
                    }
                }

                // Raise the DownloadCompleted event when the download is completed
                IsBusy = false;
                OnDownloadCompleted(new(null, false, new object()));
            }
            catch (Exception ex)
            {
                // Raise the DownloadErrorOccurred event if an error occurs
                IsBusy = false;
                Program.Log?.Write(LogEventLevel.Error, $"Couldn't download `{url}` as `{destinationPath}` from byte {startByte} for length of {endByte} bytes", ex);
                OnDownloadErrorOccurred($"Error downloading file: {ex.Message} from byte {startByte} for length of {endByte} bytes", ex);
            }
            finally
            {
                // Set the IsBusy flag to false when the download is completed
                Program.Log?.Write(LogEventLevel.Information, $"File from URL `{url}` is saved as `{destinationPath}` from byte {startByte} for length of {endByte} bytes");
                IsBusy = false;
            }
        }


        /// <summary>
        /// Get File size from URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public long GetFileSizeFromUrl(string url)
        {
            if (url.Contains("github.com"))
            {
                url = url.Replace("github.com", "raw.githubusercontent.com");
                url = url.Replace("/tree/", "/");
                url = url.Replace("/blob/", "/");
                url = url.Replace("?raw=true", string.Empty);
            }

            long result = 0;

            try
            {
                using (HttpResponseMessage response = client.SendAsync(new(HttpMethod.Head, url)).Result)
                {
                    response.EnsureSuccessStatusCode();

                    result = response.Content.Headers.ContentLength.GetValueOrDefault();
                }

                Program.Log?.Write(LogEventLevel.Information, $"File size from URL `{url}` is `{result}` bytes");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Couldn't get file size from `{url}`", ex);
                OnDownloadErrorOccurred($"Error getting file size from URL: {ex.Message}", ex);
                result = 0;
            }

            return result;
        }

        /// <summary>
        /// Pause the currently running download
        /// </summary>
        public void PauseDownload()
        {
            lock (lockObject)
            {
                Program.Log?.Write(LogEventLevel.Information, $"Download is paused.");
                cancellationTokenSource?.Cancel();
            }
        }

        /// <summary>
        /// Stops the current download operation.
        /// </summary>
        /// <remarks>This method cancels the ongoing download operation and raises the download completion event if the
        /// operation was in progress. It ensures thread safety by locking the operation during execution.</remarks>
        /// <param name="canceled">A value indicating whether the download was canceled by the user. If <see langword="true"/>, the operation is marked
        /// as canceled; otherwise, it is stopped normally.</param>
        public void StopDownload(bool canceled = false, bool disposed = false)
        {
            lock (lockObject)
            {
                Program.Log?.Write(LogEventLevel.Information, $"Download {(disposed ? "manager has been disposed" : (canceled ? "canceled" : "finished"))}.");
                cancellationTokenSource?.Cancel();
                if (IsBusy) OnDownloadCompleted(new(null, true, new object()));
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
        protected virtual void OnDownloadCompleted(AsyncCompletedEventArgs e)
        {
            DownloadFileCompleted?.Invoke(this, e);
        }

        /// <summary>
        /// Method to raise the DownloadErrorOccurred event
        /// </summary>
        /// <param name="errorMessage"></param>
        protected virtual void OnDownloadErrorOccurred(string errorMessage, Exception ex)
        {
            DownloadErrorOccurred?.Invoke(this, errorMessage);
            Forms.BugReport.ThrowError(ex);
        }

        #endregion

        #region Classes

        /// <summary>
        /// Event arguments for the DownloadProgressChanged event
        /// </summary>
        /// <remarks>
        /// Constructor for DownloadProgressEventArgs
        /// </remarks>
        /// <param name="bytesDownloaded"></param>
        /// <param name="totalBytes"></param>
        public class DownloadProgressEventArgs(long bytesDownloaded, long totalBytes) : EventArgs
        {
            /// <summary>
            /// Bytes received so far by download manager
            /// </summary>
            public long BytesReceived { get; } = bytesDownloaded;

            /// <summary>
            /// Total bytes to receive. It could be zero if the server doesn't return the content length.
            /// </summary>
            public long TotalBytesToReceive { get; } = totalBytes;

            /// <summary>
            /// Progress percentage of the download
            /// </summary>
            public long ProgressPercentage => TotalBytesToReceive > 0 ? BytesReceived * 100 / TotalBytesToReceive : 0;
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Dispose the download manager
        /// </summary>
        public void Dispose()
        {
            StopDownload(false, true);
            client?.Dispose();
            cancellationTokenSource?.Dispose();
        }

        #endregion
    }
}
