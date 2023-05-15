namespace LogoTransfer.Core.Services
{
    public interface IImportService
    {
        public Task SaveOrdersAsync();
        public Task SaveProductsAsync();
        public void StartAsync(Object state);
    }
}
