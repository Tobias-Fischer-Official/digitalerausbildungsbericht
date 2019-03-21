using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Updater
{
    [Serializable]
    public class UpdateConfig    
    {
        public UpdateConfig()
        {

        }
        public List<UpdateFile> Files;
        public string Version;
        public string Mincompat;

        public static UpdateConfig Deserialize(string config)
        {
            XmlSerializer XmlSe = new XmlSerializer(typeof(UpdateConfig));
            StringReader reader = new StringReader(config);
            return (UpdateConfig)XmlSe.Deserialize(reader);
        }
    }

    [Serializable]
    public class UpdateFile
    {
        public UpdateFile()
        {

        }
        public string FileName;   
        public string Folder;
        public Boolean Overwrite;
    }    
}
