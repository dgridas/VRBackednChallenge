namespace HdrBoxReader.BLL
{
    public interface IHdrDataProcessor
    {
        void ProcessHdrFile(string inputFile, int chunkSize);
    }
}