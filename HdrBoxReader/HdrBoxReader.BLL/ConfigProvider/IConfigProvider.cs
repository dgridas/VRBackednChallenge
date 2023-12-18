using Microsoft.Extensions.Configuration;

namespace Employee.BLL.ConfigProvider
{
    public interface IConfigProvider
    {
        string ConnectionString { get; }
        string ListeningFolder { get; }
        int ChunkSize { get; }
        string FileMask { get; }
        string CompletedFilesFolder { get; }

        IConfiguration AppSetting { get; }
    }
}