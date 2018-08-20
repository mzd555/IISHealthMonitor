using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Tx.Windows;

/// <summary>
/// Summary description for LogProvider
/// </summary>
public class LogProvider
{
    public static IEnumerable<W3CEvent> GetLogs()
    {
        var logPath = ConfigurationManager.AppSettings["LogPath"];
        var files = new DirectoryInfo(logPath).GetFiles();
        var latest = files.OrderByDescending(f => f.CreationTime).FirstOrDefault();
        var copy = latest.FullName + "copy";
        File.Copy(latest.FullName, copy);//because IIS has lock on these files
        var iisLog = W3CEnumerable.FromFile(copy);
        var logs = iisLog.ToList();
        File.Delete(copy);
        return logs.OrderByDescending(o => o.dateTime);
    }
}