using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikeWGETLib
{
    public interface IDownloader
    {
        Task<string> DownloadAsync(string address);

        Task<string> DownloadAsync(string address, CancellationToken token);

        string Download(string address);        
    }
}
