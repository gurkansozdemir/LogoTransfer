namespace LogoTransfer.Core.Services
{
    public interface IImportService
    {
        public Task SaveOrdersAsync();
        public Task SaveProductsAsync();
    }
}
