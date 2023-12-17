using HdrBoxReader.BLL.DataServices;
using HdrBoxReader.BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HdrBoxReader.BLL
{
    public class HdrDataProcessor : IHdrDataProcessor
    {
        private HdrFileReader _fileReader;
        private HdrParser _lineParser;
        private HdrKeeper _dataKeeper;

        public HdrDataProcessor()
        {
            _fileReader = new HdrFileReader();
            _lineParser = new HdrParser();
            _dataKeeper = new HdrKeeper();

        }
        public void ProcessHdrFile(string inputFile, int chunkSize)
        {
            string line;
            HdrBox? hdrBox = null;

            _fileReader.Init(inputFile);
            _dataKeeper.Init(chunkSize);

            do
            {
                line = _fileReader.GetLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    if (line.StartsWith("HDR"))
                    {
                        if (hdrBox != null) StoreExistingBox(hdrBox); //keep previous if exists
                        hdrBox = _lineParser.ParseHDRBox(line);
                    }
                    else if (line.StartsWith("LINE") && hdrBox != null)
                    {
                        hdrBox.Contents.Add(_lineParser.ParseBoxLine(line));
                    }

                    Console.WriteLine(line);
                }
            } while (!_fileReader.IsEndOfFileReached);
            StoreExistingBox(hdrBox, true);
        }

        private void StoreExistingBox(HdrBox? incomingHdrBox, bool isFinal = false)
        {
            if (incomingHdrBox != null)
            {
                _dataKeeper.PersistIncomingHdrBox(incomingHdrBox, isFinal);
            }
        }

    }
}
