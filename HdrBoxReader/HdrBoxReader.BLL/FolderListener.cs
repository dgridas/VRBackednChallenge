namespace HdrBoxReader.BLL
{
    public class FolderListener : IFolderListener
    {
        public List<string> GetAwaitingFiles(string targetPath, string fileMask)
        {
            return Directory.GetFiles(targetPath, fileMask).ToList();
        }

        public bool MoveFileForProccesing(string sourceFile, string destination)
        {
            try
            {
                Directory.Move(sourceFile, destination);
            }
            catch (Exception ex)
            {
                return false;
                //log error into Logger;
            }
            return true;
        }

    }
}