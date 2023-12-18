using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BLL.ConfigProvider
{
    public class ConfigProvider : IConfigProvider
    {

        public string ConnectionString { get { return GetConenctionString(); } }
        public string CompletedFilesFolder { get { return GetCompletedFilesFolder(); } }

        public string ListeningFolder { get { return GetListeningFolder(); } }

        public string FileMask { get { return GetFileMask(); } }
        
        public int ChunkSize { get { return GetChunkSize(); } }
        public IConfiguration AppSetting { get { return GetAppSettingsFromFile(); } }

        private string GetConenctionString()
        {
            var result = AppSetting.GetSection("DbSettings")["ConnectionString"];
            return result;
        }

        private string GetListeningFolder()
        {
            var result = AppSetting.GetSection("FileSystem")["ListeningFolder"];
            return result;
        }

        private string GetCompletedFilesFolder()
        {
            var result = AppSetting.GetSection("FileSystem")["CompletedFilesFolder"];
            return result;
        }

        private string GetFileMask()
        {
            var result = AppSetting.GetSection("FileSystem")["FileMask"];
            return result;
        }

        private int GetChunkSize()
        {
            var result = int.Parse(AppSetting.GetSection("DbSettings")["BatchChunkSize"]);
            return result;
        }


        private IConfiguration GetAppSettingsFromFile()
        {

            var result = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            return result.Build();
        }
    }
}
