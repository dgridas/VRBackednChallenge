namespace HdrBoxReader.BLL
{
    public interface IFolderListener
    {
        List<string> GetAwaitingFiles(string targetPath, string fileMask);
        void MoveFileIntoProcessedFolder(string inputFile);
    }
}