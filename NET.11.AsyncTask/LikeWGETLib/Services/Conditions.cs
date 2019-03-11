using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikeWGETLib
{
    public enum Restrictions
    {
        WithoutRestrictions = 0,
        OnlyInsideCurrent,
        NotUpperSourceUrl
    }

    public class Conditions : IConditions
    {
        private string domain;

        private Uri sourcePath;

        public Conditions(Uri sourcePath)
        {
            Extensions = new List<string>();
            SourcePath = sourcePath;
        }

        public Restrictions DomainCondition { get; set; }

        public List<string> Extensions { get; set; }

        public Uri SourcePath
        {
            get
            {
                return sourcePath;
            }

            set
            {
                sourcePath = value ?? throw new ArgumentNullException($"Source path {nameof(value)} has null value.");
            }
        }

        public string Domain
        {
            get { return domain; }
            set { domain = value ?? throw new ArgumentNullException($"Domain {nameof(value)} has null value.");  }
        }

        public List<Uri> UseConditions(IEnumerable<Uri> uris)
        {
            var resultUris = new List<Uri>();

            foreach (var item in uris)
            {
                if (IsValidByExtension(item) && IsValidByDomain(item))
                {
                    resultUris.Add(item);
                }
            }

            return resultUris;
        }

        public bool IsValidByDomain(Uri uriToAdd)
        {
            if (uriToAdd != null)
            {
                switch (DomainCondition)
                {
                    case Restrictions.WithoutRestrictions:
                        {
                            return true;
                        }

                    case Restrictions.OnlyInsideCurrent:
                        {
                            return uriToAdd.DnsSafeHost.Remove(0, uriToAdd.DnsSafeHost.IndexOf('.')) == Domain;
                        }

                    case Restrictions.NotUpperSourceUrl:
                        {
                            string[] segments = uriToAdd.Segments;
                            if (segments.Length < SourcePath.Segments.Length)
                            {
                                return false;
                            }

                            int i = 0;
                            foreach (var item in SourcePath.Segments)
                            {
                                if (item != segments[i])
                                {
                                    return false;
                                }

                                i++;
                            }
                        }

                        break;
                    default: return false;
                }
            }

            return false;
        }

        public bool IsValidByExtension(Uri uriToAdd)
        {
            if (uriToAdd != null)
            {
                if (uriToAdd.OriginalString.LastIndexOf('.') >= 0)
                {
                    string extension = uriToAdd.OriginalString.Remove(0, uriToAdd.OriginalString.LastIndexOf('.'));

                    if (Extensions.Contains(extension.Replace(".", string.Empty)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
