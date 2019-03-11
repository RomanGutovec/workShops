using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikeWGETLib
{
    public class DownloadService : IDownloader
    {
        public async Task<string> DownloadAsync(string address)
        {
            using (var client = new HttpClient())
            {

                string html;

                try
                {
                    var responseMessage = await client.GetAsync(address);
                    html = await responseMessage.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    html = e.ToString();
                }

                return html;
            }
        }

        public async Task<string> DownloadAsync(string address, CancellationToken token)
        {
            using (var client = new HttpClient())
            {
                string html;

                try
                {
                    var responseMessage = await client.GetAsync(address, token);
                    html = await responseMessage.Content.ReadAsStringAsync();
                }
                catch (TaskCanceledException)
                {
                    html = "Downloading has been cancelled.";
                }
                catch (Exception e)
                {
                    html = e.ToString();
                }

                return html;
            }
        }

        public string Download(string address)
        {
            using (var client = new WebClient())
            {

                string html;

                try
                {
                    html = client.DownloadString(address);
                }
                catch (Exception e)
                {
                    html = e.ToString();
                }

                return html;
            }
        }
    }
}
