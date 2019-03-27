using System;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using NLog.Config;
using NLog.Targets;
using LoggingSample_Logs_DAL.Entities;
using System.IO;
using System.Xml.Serialization;

namespace LoggingSample_BLL.LogTargets
{
    [Target("XmlTarget")]
    public class XmlTargets : AsyncTaskTarget
    {
        public XmlTargets()
        {
            this.Host = Environment.MachineName;
        }

        [RequiredParameter]
        public string Host { get; set; }

        protected override async Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken cancellationToken)
        {
            var logMessage = new LogMessage
            {
                MachineName = this.Host,
                Exception = logEvent.Exception?.ToString(),
                LoggerName = logEvent.LoggerName,
                Level = logEvent.Level.ToString(),
                Message = logEvent.Message,
                MessageSource = logEvent.CallerFilePath,
                TimeStamp = logEvent.TimeStamp
            };

            XmlSerializer formatter = new XmlSerializer(typeof(LogMessage));

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"./Logs/{logMessage.TimeStamp.ToShortDateString()}.xml");

            await Task.Run(() =>
            {
                using (FileStream fs = new FileStream(path, FileMode.Append))
                {
                    formatter.Serialize(fs, logMessage);
                }

            });
        }

        private string GetLogFileName(string targetName)
        {
            string fileName = null;

            if (LogManager.Configuration != null && LogManager.Configuration.ConfiguredNamedTargets.Count != 0)
            {
                Target target = LogManager.Configuration.FindTargetByName(targetName);
                if (target == null)
                {
                    throw new Exception("Could not find target named: " + targetName);
                }

                FileTarget fileTarget = null;

                var logEventInfo = new LogEventInfo { TimeStamp = DateTime.Now };
                fileName = fileTarget.FileName.Render(logEventInfo);
            }
            else
            {
                throw new Exception("LogManager contains no Configuration or there are no named targets");
            }

            if (!File.Exists(fileName))
            {
                throw new Exception("File " + fileName + " does not exist");
            }

            return fileName;
        }

    }
}