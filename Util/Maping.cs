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
        public string path;
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
                watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastAccess | NotifyFilters.LastWrite
                         | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                watcher.Deleted += new FileSystemEventHandler(OnDeleted);
                path = item;
                watcher.Created += new FileSystemEventHandler(OnCreated);
                watcher.Renamed += new RenamedEventHandler(OnChanged);
                watcher.Filter = "*.*";
                watcher.EnableRaisingEvents = true;
            }
        }
        private void LogToFile(string message) {
            try
            {
            if (!File.Exists(logPath + @"\log.txt"))
            {
                File.Create(logPath + @"\log.txt").Dispose();
                using (var writer = new StreamWriter(logPath + @"\log.txt", false))
                {
                    writer.WriteLine("{0} - {1}", DateTime.Now, "Log criado com sucesso");
                    writer.WriteLine("{0} - {1}", DateTime.Now, message);
                }
            }
            else {
                using (var writer = new StreamWriter(logPath + @"\log.txt", true))
                {
                writer.WriteLine("{0} - {1}", DateTime.Now, message);
                }
            }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            if (cont == 1)
            {
                if (File.Exists(e.FullPath))
                {
                    // É um evento de criação de arquivo
                    string created = $"Created Archive: {e.FullPath}";
                    LogToFile(created);
                }
                else if (Directory.Exists(e.FullPath))
                {
                    if (Path.GetExtension(e.FullPath) == "")
                    {
                        string created = $"Folder Created: {e.FullPath}";
                        LogToFile(created);
                    }
                }
                cont = 0;
            }
            else {
                string changed = $"File Renamed: {e.FullPath}";
                LogToFile(changed);
            }
            
        }
        private void Created(object source,FileSystemEventArgs e ) {
               if (File.Exists(e.FullPath))
               {
                    // É um evento de criação de arquivo
                   string created = $"Created Archive: {e.FullPath}";
                   LogToFile(created);
                }
                else if(Directory.Exists(e.FullPath))
                      {
                          if (Path.GetExtension(e.FullPath) == "")
                         {
                              string created = $"Folder Created: {e.FullPath}";               
                              LogToFile(created);
                         }                    
                      }             
        }
        
        private void OnCreated(object source, FileSystemEventArgs e )
        {
            cont = 1;
        }

  
        private void OnDeleted(object source, FileSystemEventArgs e)
        {
           string delete = $"File Deleted: {e.FullPath}";
            LogToFile(delete);

        }


    }
}
