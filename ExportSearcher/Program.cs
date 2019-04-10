using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ExportSearcher
{
    class Program
    {
        struct FileHeader
        {
            public ushort f_magic;
            public ushort f_nscns;
            public int f_timedat;
            public int f_symptr;
            public int f_nsyms;
            public ushort f_opthdr;
            public ushort f_flags; 
        }

        static T castToStructure<T>(FileStream fs)
        {
            int toRead = Marshal.SizeOf<T>();
            byte[] buffer = new byte[toRead];
            fs.Read(buffer, 0, toRead);
            unsafe
            {
                fixed(void* ptr = &buffer[0])
                {
                    T s = Marshal.PtrToStructure<T>(new IntPtr(ptr));
                    return s; 
                }
            }
        }

        static void Main(string[] args)
        {
            string toFind = args[0];
            string directoryToSearch = args[1];

            List<string> pathsThatMatch = new List<string>(); 
            foreach(string file in Directory.GetFiles(directoryToSearch, "*.lib"))
            {
                byte[] buffer = new byte[56]; 
                using (FileStream fs = File.OpenRead(file))
                {
                    fs.Read(buffer, 0, 16); 
                    bool foundEnd
                    for(int c=0;c<buffer.Length;c++)
                    {
                        if(buffer[c] == 0x20)
                        {
                            return; 
                        }
                    }
                    if (Encoding.ASCII.GetString(buffer).Contains("!<arch>"))
                    {
                        //https://en.wikipedia.org/wiki/Ar_(Unix)
                        fs.Seek(56, SeekOrigin.Begin);
                        fs.Read(buffer, 0, 10); 
                        string lengthBytes = Encoding.ASCII.GetString(buffer);
                    }
                    else
                    {

                    }
                }


                //    string fileContent = File.ReadAllText(file); 
                //if(fileContent.Contains(toFind))
                //{
                //    pathsThatMatch.Add(file); 
                //}
            }

            if(pathsThatMatch.Count == 0)
            {
                Console.WriteLine("None match, sorry."); 
            }
            else
            {
                Console.WriteLine("Try these:"); 
                foreach(string file in pathsThatMatch)
                {
                    Console.WriteLine($"\t{file}"); 
                }
            }

            Console.ReadLine(); 
        }
    }
}
