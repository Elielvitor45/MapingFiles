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
        public int cont = 0;
        public string logPath = "C:\\Users\\Playlist\\Documents\\testelog";
        public Maping(List<FolderOB> path) { 
            
            foreach (var item in path)
            {
                Folders_string.Add(item.FolderPath);

            }
        }
        public static void CreateLog(string logFilePath)
        {
            var writer = File.CreateText(logFilePath+@"\log.txt");
                writer.WriteLine("Arquivo de Log Iniciado: {0}", DateTime.Now);
                writer.Close();
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
                if (cont == 0)
                {
                    watcher.Created += new FileSystemEventHandler(OnCreated);
                }
                else { watcher.Created += new FileSystemEventHandler(OnChanged); }

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
            if (e.Name.Length >= 3 && e.Name.Substring(0, 3) == "Novo")
            {
                cont = 1;
            }
            else
            {
                string created = $"File Created: {e.FullPath}";
                LogToFile(created);
                cont = 0;
            }
        }
        private void OnDeleted(object source, FileSystemEventArgs e)
        {
           string delete = $"File Deleted: {e.FullPath}";
            LogToFile(delete);

        }


    }
}
