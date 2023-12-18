using HdrBoxReader.BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HdrBoxReader.BLL
{
    public class HdrFileReader : IHdrFileReader
    {
        private StreamReader _readingStream;
        public bool IsEndOfFileReached
        {
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
                _readingStream.Close();
            }
            return line;
        }

    }
}
