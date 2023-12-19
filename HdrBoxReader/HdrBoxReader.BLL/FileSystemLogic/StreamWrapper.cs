using HdrBoxReader.BLL.Interfaces;

namespace HdrBoxReader.BLL
{
    public class StreamWrapper : IStreamWrapper
    {
        public StreamReader StreamReader(string path)
        {
            return new StreamReader(path);
        }
    }
}
