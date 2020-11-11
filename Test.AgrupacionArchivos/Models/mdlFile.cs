using System;

namespace Test.AgrupacionArchivos.Models
{
    public class mdlFile
    {
        public string fileName { get; set; }
        public string extension { get; set; }
        public DateTime creationFile { get; set; }
        public long filesize { get; set; }
        public string pathFile { get; set; }
        public string UID { get; set; } 
        public string varfecha { get; set; }

        public mdlFile()
        {
            fileName = string.Empty;
            extension = string.Empty;
            creationFile = DateTime.MinValue;
            filesize = int.MinValue;
            UID = string.Empty;
            pathFile = string.Empty;
            varfecha = string.Empty;
        }
    }
}
