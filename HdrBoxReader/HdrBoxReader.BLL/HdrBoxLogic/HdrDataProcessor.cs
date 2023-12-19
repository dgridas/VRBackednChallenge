using Employee.BLL.ConfigProvider;
using HdrBoxReader.BLL.DataServices;
using HdrBoxReader.BO.Constants;
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
        private IHdrFileReader _fileReader;
        private IHdrParser _lineParser;
        private IHdrKeeper _dataKeeper;
        private IConfigProvider _configProvider;

        public HdrDataProcessor(IHdrFileReader fileReader, IHdrParser lineParser, IHdrKeeper dataKeeper, IConfigProvider configProvider)
        {
            _fileReader = fileReader;
            _lineParser = lineParser;
            _dataKeeper = dataKeeper;
            _configProvider = configProvider;
        }
        public void ProcessHdrFile(string inputFile, int chunkSize)
        {
            string line;
            HdrBox? hdrBox = null;

            _fileReader.Init(inputFile);
            _dataKeeper.Init();

            do
            {
                line = _fileReader.GetLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    if (line.StartsWith(Constants.BoxPrefix))
                    {
                        if (hdrBox != null) StoreExistingBox(hdrBox); //keep previous if exists
                        hdrBox = _lineParser.ParseHDRBox(line);
                    }
                    else if (line.StartsWith(Constants.ContentPrefix) && hdrBox != null)
                    {
                        var hdrContent = _lineParser.ParseBoxLine(line);
                        if (hdrContent != null) hdrBox.Contents.Add(hdrContent);
                    }
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
