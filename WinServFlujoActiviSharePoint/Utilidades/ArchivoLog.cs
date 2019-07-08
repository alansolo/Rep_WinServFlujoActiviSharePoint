﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades

{
    public class ArchivoLog
    {
        public static void EscribirLog(string path, string text)
        {
            string pathDefault = System.AppDomain.CurrentDomain.BaseDirectory;
            pathDefault = pathDefault.Replace("bin\\Debug\\", "");

            if (path == null || path == string.Empty)
            {
                path = pathDefault + "Log/Log_" + DateTime.Now.ToString("dd_MM_yyyy");
            }
            else
            {
                path = path + "/Log/Log_" + DateTime.Now.ToString("dd_MM_yyyy");
            }

            if (File.Exists(path))
            {
                using (StreamWriter escribir = File.AppendText(path))
                {
                    escribir.WriteLine(text);
                }
            }
            else
            {
                using (StreamWriter escribir = File.CreateText(path))
                {
                    escribir.WriteLine(text);
                }

            }

        }
    }
}
