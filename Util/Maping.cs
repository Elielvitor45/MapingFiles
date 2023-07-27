using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FolderTracker.Util
{
    public class Maping
    {
        public List<string> Folders_string { get; set; } = new List<string>();
        public string logPath = "C:\\Users\\Playlist\\Documents\\testelog";
        public Maping(List<FolderOB> path) { 
            
            foreach (var item in path)
            {
                Folders_string.Add(item.FolderPath);

            }
        }
        public void map()
        {
            foreach (var item in Folders_string)
            {
                var watcher = new FileSystemWatcher();
                watcher.Path = item;
                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                         | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                watcher.Changed += new FileSystemEventHandler(OnChanged);
                watcher.Created += new FileSystemEventHandler(OnCreated); 
                watcher.Deleted += new FileSystemEventHandler(OnDeleted);
                watcher.EnableRaisingEvents = true;
            }
        }
        private void LogToFile(string message) {
            
            using (var writer = new StreamWriter(logPath + @"\log.txt", true))
            {
                writer.WriteLine("{0} - {1}", DateTime.Now, message);

            }
        }
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            string changed = $"File Renamed: {e.FullPath}";
            LogToFile(changed);
        }
        private void OnCreated(object source, FileSystemEventArgs e)
        {
                string created = $"File Created: {e.FullPath}";
                LogToFile(created);
        }
        private void OnDeleted(object source, FileSystemEventArgs e)
        {
           string delete = $"File Deleted: {e.FullPath}";
            LogToFile(delete);

        }


    }
}
