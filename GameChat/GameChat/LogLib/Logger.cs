using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameChat.LogLib
{
    public class Logger
    {
        public void Log(string log)
        {
            //Creating or opening the file to write to
            FileStream file = new FileStream("log.txt", FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter myStreamWriter = new StreamWriter(file);

            myStreamWriter.BaseStream.Seek(0, SeekOrigin.End);
            myStreamWriter.Write(log);

            //Closing the StreamWriter
            myStreamWriter.Close();
        }
    }
}
