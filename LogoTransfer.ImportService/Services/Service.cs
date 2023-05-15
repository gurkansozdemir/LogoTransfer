using LogoTransfer.Core.Services;

namespace LogoTransfer.ImportService.Services
{
    public class Service
    {
        public readonly IImportService _importService;

        public Service(IImportService importService)
        {
            _importService = importService;
        }

        public static void StartAsync(Object state)
        {

        }
    }
}
