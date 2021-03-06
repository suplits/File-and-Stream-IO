﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace ZipStream
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream outStream = new MemoryStream();
            using (ZipFile zip = new ZipFile())
            {
                string[] files = Directory.GetFiles(@"C:\ForRobot");
                zip.AddFiles(files,"");
                zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                //zip.Save(@"C:\ForRobot.zip");
                zip.Save(outStream);
             }
            
            Console.WriteLine(outStream.Length);
            outStream.Close();
            Console.ReadLine(); 
        }
        
    }
}
