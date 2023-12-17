using HdrBoxReader.BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HdrBoxReader.BLL
{
    public class HdrFileReader
    {
        private StreamReader _readingStream;
        public bool IsEndOfFileReached {
            get; 
            private set;
        }

        public void Init(string inputFilePath)
        {
            IsEndOfFileReached = false;
            _readingStream = new StreamReader(inputFilePath);
        }

        public string GetLine()
        {

            var line = _readingStream.ReadLine();
            if (line == null) 
            {
                IsEndOfFileReached = true;
            }
            return line;

            //using (StreamReader sr = new StreamReader(inputFilePath))
            //{
            //    string line;
            //    HdrBox hdrBox = null;

            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        if (line.StartsWith("HDR"))
            //        {
            //            if (hdrBox != null) StoreExistingBox(hdrBox);
            //            hdrBox = ParseHDRBox(line);
            //        }
            //        else if (line.StartsWith("LINE"))
            //        {
            //            hdrBox.Contents.Add(ParseBoxLine(line));
            //        }

            //        Console.WriteLine(line);
            //    }
            //    StoreExistingBox(hdrBox);
            //}
            //return true;
        }

    }
}
