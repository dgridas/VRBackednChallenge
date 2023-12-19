using HdrBoxReader.BLL.Interfaces;
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
        private StreamReader? _readingStream;

        private IStreamWrapper _fileStream;


        public bool IsEndOfFileReached
        {
            get;
            private set;
        }

        public HdrFileReader(IStreamWrapper streamWrapper)
        {
            _fileStream = streamWrapper;
        }

        public void Init(string inputFilePath)
        {
            IsEndOfFileReached = false;
            _readingStream = _fileStream.StreamReader(inputFilePath);
        }

        public string GetLine()
        {
            if (_readingStream != null)
            {
                var line = _readingStream.ReadLine();
                if (line == null)
                {
                    IsEndOfFileReached = true;
                    _readingStream.Close();
                    _readingStream = null;
                }
                return line;
            }
            return string.Empty;
        }

    }
}
