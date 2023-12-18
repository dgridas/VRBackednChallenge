namespace HdrBoxReader.BLL
{
    public interface IHdrFileReader
    {
        bool IsEndOfFileReached { get; }

        string GetLine();
        void Init(string inputFilePath);
    }
}