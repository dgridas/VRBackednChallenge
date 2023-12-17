namespace HdrBoxReader.BLL
{
    public interface IFolderListener
    {
        List<string> GetAwaitingFiles(string targetPath, string fileMask);
        bool MoveFileForProccesing(string sourceFile, string destination);
    }
}