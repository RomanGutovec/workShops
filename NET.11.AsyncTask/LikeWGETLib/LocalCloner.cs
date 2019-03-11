using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CsQuery;

namespace LikeWGETLib
{
    /// <summary>
    /// Represents an opportunity to clone data from the webpage.
    /// </summary>
    public class LocalCloner
    {
        private readonly IConditions conditions;
        private readonly IDownloader service;

        /// <summary>
        /// Creates instance of the <see cref="LocalCloner"/> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when conditions or service to download file has null value.</exception>
        /// <param name="conditions">Conditions according to which data are cloned.</param>
        /// <param name="service">Service which has an opportunity to download data from pages.</param>
        public LocalCloner(IConditions conditions, IDownloader service)
        {
            this.conditions = conditions ?? throw new ArgumentNullException($"Conditions {nameof(conditions)} has null value.");
            this.service = service ?? throw new ArgumentNullException($"Service to download {nameof(service)}files has null value.");
        }

        /// <summary>
        /// Clones necessary data from the webpage.
        /// </summary>
        /// <param name="path">Path from which is going to clone data.</param>
        /// <param name="pathToSave">Directory to save data from webpage.</param>
        /// <param name="level">Level of cloning.</param>
        /// <returns>Task.</returns>
        public async Task Clone(string path, string pathToSave, int level)
        {
            var uries = new List<Uri>() { new Uri(path) };
            uries.AddRange(ParseSource(path));

            if (level == 0)
            {
                await Clone(path, pathToSave, uries).ConfigureAwait(false);
            }
            else
            {
                uries.AddRange(ParseLinks(path));

                foreach (var item in uries)
                {
                    Console.WriteLine("Works....");

                    Console.WriteLine(pathToSave + $"{ item.OriginalString.Replace("/", "")}");
                    if (!item.IsAbsoluteUri)
                    {
                        await Clone("http://" + new Uri(path).Host + item.OriginalString, pathToSave, level - 1).ConfigureAwait(false);
                    }
                    else
                    {
                        await Clone(item.OriginalString, pathToSave, level - 1).ConfigureAwait(false);
                    }

                    Console.WriteLine(item);
                }
                --level;
            }
        }

        /// <summary>
        /// Clones necessary data from the webpage.
        /// </summary>
        /// <param name="path">Path from which is going to clone data.</param>
        /// <param name="pathToSave">Directory to save data from webpage.</param>
        /// <param name="uris">List of the identifiers is going to clone.</param>
        /// <returns>Task.</returns>
        public async Task Clone(string path, string pathToSave, List<Uri> uris)
        {
            var sourceUri = new Uri(path);
            WebClient client = new WebClient();

            foreach (var uri in uris)
            {
                if (uri.IsAbsoluteUri)
                {
                    string str = pathToSave + $"{ uri.OriginalString.Replace("/", "").Replace(":", "").Replace(".", "").Replace("?", "")}";
                    var result = await service.DownloadAsync(uri.OriginalString);

                    await WriteAsync(pathToSave + $"{ uri.OriginalString.Replace("/", "").Replace(":", "").Replace(".", "").Replace("?", "").Replace("=", "")}", result).ConfigureAwait(false);
                }
                else
                {
                    string directoryToSave = pathToSave.Remove(pathToSave.Length - 1, 1) + $"{uri.OriginalString.Replace("/", "\\")}";

                    if (directoryToSave.LastIndexOf('\\') >= 0 && directoryToSave.LastIndexOf('\\') != uri.OriginalString.Length)
                    {
                        directoryToSave = directoryToSave.Remove(directoryToSave.LastIndexOf('\\') + 1, directoryToSave.Length - directoryToSave.LastIndexOf('\\') - 1);
                    }

                    if (!Directory.Exists(directoryToSave))
                    {
                        Directory.CreateDirectory(directoryToSave);
                    }

                    var result = await service.DownloadAsync("http://" + sourceUri.Host + uri.OriginalString).ConfigureAwait(false);

                    await WriteAsync(
                        directoryToSave + $@"{ uri.OriginalString.Remove(0, 1)
                        .Replace("/", "")
                        .Replace("=", "")
                        .Replace("&", "")
                        .Replace("?", "")}", result).ConfigureAwait(false);
                }
            }
        }

        private List<Uri> ParseLinks(string path)
        {
            var dom = CQ.CreateFromUrl(path);

            List<Uri> uries = new List<Uri>();

            foreach (IDomObject obj in dom.Find("a"))
            {
                if (Uri.IsWellFormedUriString(obj.GetAttribute("href"), UriKind.Absolute))
                {
                    Uri.TryCreate(obj.GetAttribute("href"), UriKind.Absolute, out Uri uriToAdd);
                    if (uriToAdd != null)
                    {
                        if (conditions.IsValidByDomain(uriToAdd))
                        {
                            uries.Add(uriToAdd);
                        }
                    }
                }

                Console.WriteLine("Parse links finished");
            }

            return uries.Distinct().ToList();
        }

        private List<Uri> ParseSource(string path)
        {
            var dom = CQ.CreateFromUrl(path);

            List<Uri> uries = new List<Uri>();

            foreach (IDomObject obj in dom.Find("img"))
            {
                Console.WriteLine(obj.GetAttribute("src"));
                if (Uri.IsWellFormedUriString(obj.GetAttribute("src"), UriKind.RelativeOrAbsolute))
                {
                    Uri.TryCreate(obj.GetAttribute("src"), UriKind.RelativeOrAbsolute, out Uri uriToAdd);
                    if (conditions.IsValidByExtension(uriToAdd))
                    {
                        uries.Add(uriToAdd);
                    }
                }

                Console.WriteLine("Parse source finished");
            }

            return uries.Distinct().ToList();
        }

        private async Task WriteAsync(string path, string data)
        {
            using (var sw = new StreamWriter(path))
            {
                await sw.WriteAsync(data).ConfigureAwait(false);
            }
        }
    }
}
