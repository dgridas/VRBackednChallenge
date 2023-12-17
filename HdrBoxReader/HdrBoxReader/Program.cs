using HdrBoxReader.BLL;

namespace HdrBoxReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var listener = new FolderListener();
            var files = listener.GetAwaitingFiles("C:\\DataFiles", "data.txt");
            
            foreach (var fileItem in files)
            {
                var hdrParser = new HdrDataProcessor();
                hdrParser.ProcessHdrFile(fileItem, 3);
            }
        }
    }
}