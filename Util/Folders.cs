using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FolderTracker.Util
{
    public class Folders
    {

        public string Path { get; set; }
        public List<FolderOB> folderOBs { get; set; } = new List<FolderOB>();
        public Folders(string path) {
            Path = @path;
            Path += @"\Folders.xml";
            AddListFolderOB();
            StartMaping();
        }
        public void StartMaping()
        {
            Maping maping = new Maping(folderOBs);
            maping.map();
        }




        public void AddListFolderOB()
        {
            XmlNodeList xmlNodeList = getXmlNodeList();
            List<XmlNode> listFolderXml = getListXmlFolder(xmlNodeList);
            folderOBs = getListObjFolderOB(listFolderXml);
        }
        public XmlNodeList getXmlNodeList()
        {
            XmlDocument xmlDocument = new XmlDocument();
            if (Directory.Exists(Path))
            {
                MessageBox.Show("Folder.xml não existe", "aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            xmlDocument.Load(Path);
            XmlNodeList NodeList = xmlDocument.SelectNodes("//Folders/*");
            return NodeList;
        }

        public List<XmlNode> getListXmlFolder(XmlNodeList folder)
        {
            
            List<XmlNode> nodeList = new List<XmlNode>();
            int cont = 0;
            foreach (XmlNode node in folder)
            {
                if (node.Name.StartsWith("Folder") && cont != 0)
                {
                    nodeList.Add(node);
                }
                cont++;
            }
            return nodeList;
        }

        public List<FolderOB> getListObjFolderOB(List<XmlNode> listFolders)
        {
            List<FolderOB> listObjFolderOB = new List<FolderOB>();
            foreach (var item in listFolders)
            {
                FolderOB objectFolder = new FolderOB(item);
                listObjFolderOB.Add(objectFolder);
            }
            return listObjFolderOB;
        } 

    }
}
