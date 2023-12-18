using Employee.BLL.ConfigProvider;
using HdrBoxReader.BLL;
using HdrBoxReader.BLL.DataServices;
using Microsoft.Extensions.DependencyInjection;

namespace HdrBoxReader
{
    internal class Program
    {
        protected readonly IHdrDataProcessor _hdrDataProcessor;
        protected readonly IFolderListener _folderListener;
        protected readonly IConfigProvider _configuration;

        public Program(IHdrDataProcessor hdrDataProcessor, IFolderListener folderListener, IConfigProvider configuration)
        {
            _hdrDataProcessor = hdrDataProcessor;
            _folderListener = folderListener;
            _configuration = configuration;
        }

        static void Main(string[] args)
        {
       

            Console.WriteLine("!----Start HdrBoxReader!------");
            Console.WriteLine("Press ESC to stop");

            IServiceProvider serviceProvider = RegisterServices();

            Program program = serviceProvider.GetService<Program>();

            program.DoProcessLogic(args);
            
            Console.WriteLine("!----End HdrBoxReader!------\n\n");

        }

        public void DoProcessLogic(string[] args)
        {
            while (true)
            {
                var files = _folderListener.GetAwaitingFiles(_configuration.ListeningFolder, _configuration.FileMask);

                if (files != null && files.Count() > 0)
                {
                    Console.WriteLine($"Found {files.Count()} files in listening folder {_configuration.ListeningFolder}");
                    foreach (var fileItem in files)
                    {
                        _hdrDataProcessor.ProcessHdrFile(fileItem, 3);
                        _folderListener.MoveFileIntoProcessedFolder(fileItem);

                        Console.WriteLine($"File {fileItem} was processed");
                    }
                }

                if (Console.KeyAvailable == true)
                {
                    var pressedKey = Console.ReadKey();
                    if (pressedKey.Key == ConsoleKey.Escape)
                        break;
                }

                Thread.Sleep(1000);
            }
        }

        private static IServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddScoped<IHdrFileReader, HdrFileReader>();
            services.AddScoped<IFolderListener, FolderListener>();
            services.AddScoped<IConfigProvider, ConfigProvider>();
            services.AddScoped<IHdrDataProcessor, HdrDataProcessor>();
            services.AddScoped<IHdrKeeper, HdrKeeper>();
            services.AddScoped<IHdrParser, HdrParser>();

            services.AddScoped<Program>();
            return services.BuildServiceProvider();
        }
    }
}