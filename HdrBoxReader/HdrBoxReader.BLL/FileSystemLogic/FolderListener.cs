using Employee.BLL.ConfigProvider;

namespace HdrBoxReader.BLL
{
    public class FolderListener : IFolderListener
    {
        private readonly IConfigProvider _configProvider;

        public FolderListener(IConfigProvider configProvider) 
        { 
            _configProvider = configProvider;
        }

        public List<string> GetAwaitingFiles(string targetPath, string fileMask)
        {
            return Directory.GetFiles(targetPath, fileMask).ToList();
        }

        public void MoveFileIntoProcessedFolder(string inputFile)
        {
            if (!Path.Exists(_configProvider.CompletedFilesFolder))
            {
                Directory.CreateDirectory(_configProvider.CompletedFilesFolder);
            }
            var f = Path.GetFileName(inputFile);
            File.Move(inputFile, _configProvider.CompletedFilesFolder + "\\" + DateTime.Now.ToString("yyyyMMdd.HHmmss.fff") + "." + f);
        }

    }
}