using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.AgrupacionArchivos.Models;

namespace Test.AgrupacionArchivos
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Lectura de carpeta. Para terminos de ejemplo simplemente partiremos de que la lista de archivos ya esta almacenada/mapeada en una lista.
                //Se crea la lista para ejemplo
                List<mdlFile> lstFiles = new List<mdlFile>();
                lstFiles = fillList();
                if (lstFiles.Count > 0)
                {
                    var lst = lstFiles.Select(x => x.varfecha).Distinct().ToList();
                    foreach (var item in lst)
                    {
                        Guid g = Guid.NewGuid();
                        lstFiles.Where(x => x.varfecha == item).ToList().ForEach(y => y.UID = g.ToString());
                    }
                }

                StringBuilder sb = new StringBuilder();
                Console.WriteLine("***--Archivos:");
                foreach (var item in lstFiles)
                {
                    string formatStringa = "File:{0} UID:{1} dateCreation:{2} \n";
                    sb.AppendFormat(formatStringa, item.fileName, item.UID, item.creationFile.ToString("dd/MM/yyyy hh:mm:ss:ff"));
                    Console.WriteLine("File:{0} UID:{1} dateCreation:{2} \n", item.fileName, item.UID, item.creationFile.ToString("dd/MM/yyyy hh:mm:ss:ff"));
                }


                foreach (var line in lstFiles.GroupBy(info => info.UID)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric))
                {
                    string formatString = "{0} {1} \n";
                    sb.AppendFormat(formatString, line.Metric, line.Count);
                    Console.WriteLine("{0} {1}", line.Metric, line.Count);
                }

                using (StreamWriter swriter = new StreamWriter(@"C:\Files\testFiles.txt"))
                {
                    swriter.Write(sb.ToString());
                }
                Console.Write("Press <Enter> to exit... ");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error en Main: " + e.Message);
            }
        }

        private static List<mdlFile> fillList()
        {
            List<mdlFile> lstFiles = new List<mdlFile>();
            try
            {
                for (int i = 0; i <= 9; i++)
                {
                    mdlFile mdl = new mdlFile();
                    mdl.creationFile = DateTime.Now;
                    mdl.varfecha = mdl.creationFile.ToString("dd/MM/yyyy hh:mm:ss");
                    mdl.extension = "pcapng";
                    mdl.fileName = "outfile_" + i.ToString();
                    Random rnd = new Random();
                    int minsize = rnd.Next(1, 20);
                    mdl.filesize = minsize * i;
                    mdl.pathFile = "c:/Repository/" + i.ToString() + "/" + mdl.fileName;
                    lstFiles.Add(mdl);

                    mdlFile mdl2 = new mdlFile();
                    mdl2.creationFile = DateTime.Now;
                    mdl2.varfecha = mdl2.creationFile.ToString("dd/MM/yyyy hh:mm:ss");
                    mdl2.extension = "pcapng";
                    mdl2.fileName = "outfileAdjunto_" + i.ToString();
                    minsize = rnd.Next(1, 20);
                    mdl2.filesize = minsize * i + 30;
                    mdl2.pathFile = "c:/Repository/" + i.ToString() + "/" + mdl2.fileName;
                    lstFiles.Add(mdl2);


                    if (IsOdd(i))
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(2000);
                    }
                }
            }
            catch (Exception ef)
            {
                Console.WriteLine("Error en fillList: " + ef.Message);
                lstFiles = null;
            }
            return lstFiles;
        }

        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }
    }
}
