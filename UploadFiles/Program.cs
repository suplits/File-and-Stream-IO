﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UploadFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            FileUploadMessage file = new FileUploadMessage();
            file.FileName = "QZhu3_QZHU3-L2-OFS_DDR-R1_setup_00000.hex";
            file.SavePath = "WellId is " + Guid.NewGuid().ToString() + @"\";
            file.FileData = new FileStream(@"c:\QZhu3_QZHU3-L2-OFS_DDR-R1_setup_00000.hex", FileMode.Open);

            UploadFile(file);
            file.FileData.Close();

            Console.WriteLine(DateTime.Now.ToShortDateString());
            Console.WriteLine(DateTime.Now.ToLongDateString());
            
            Console.ReadLine();
        }


        public static void UploadFile(FileUploadMessage request)
        {
            string uploadFolder = @"c:\Inversion\";
            string fileName = request.FileName;
            string savaPath = request.SavePath;
            string input = @"Input\";
            //string dateString = DateTime.Now.ToShortDateString() + @"\";
            
            

            Stream sourceStream = request.FileData;
            FileStream targetStream = null;

            if (!sourceStream.CanRead)
            {
                throw new Exception("Can't read!");
            }

            //if (savaPath == null) savaPath = @"Default\";
            if (!savaPath.EndsWith("\\")) savaPath += "\\";
            //uploadFolder = uploadFolder + savaPath + dateString;

            uploadFolder = uploadFolder + savaPath + input;

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            
            string filePath = Path.Combine(uploadFolder, fileName);

            using (targetStream = new FileStream(filePath, FileMode.Create))
            {
                //read from the input stream in 4K chunks
                //and save to output stream
                const int bufferLen = 4096;
                byte[] buffer = new byte[bufferLen];
                int count = 0;
                while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                {
                    targetStream.Write(buffer, 0, count);
                }

                //sourceStream.CopyTo(targetStream);

                targetStream.Close();
                sourceStream.Close();
            }
        }


    }


    public class FileUploadMessage
    {

        public string SavePath;


        public string FileName;


        public Stream FileData;

    }
}
