namespace LogoTransfer.Core.Services
{
    public interface IImportService
    {
        public Task SaveOrdersAsync();
        public void StartSync(Object sender, EventArgs e);
    }
}
