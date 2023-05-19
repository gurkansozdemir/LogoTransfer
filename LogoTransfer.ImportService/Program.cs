using LogoTransfer.Core.Repositories;
using LogoTransfer.Core.Services;
using LogoTransfer.Core.UnitOfWorks;
using LogoTransfer.ImportService.Services;
using LogoTransfer.Repository;
using LogoTransfer.Repository.Repositories;
using LogoTransfer.Repository.UnitOfWorks;
using LogoTransfer.Service.Caching;
using LogoTransfer.Service.Mapping;
using LogoTransfer.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

class Program
{
    private static System.Timers.Timer _timer;
    static async Task Main(string[] args)
    {
        await Go();
    }

    static async Task Go()
    {
        var serviceProvider = new ServiceCollection();
        serviceProvider.AddTransient<IImportService, IdeaSoftService>();
        serviceProvider.AddScoped(typeof(IOrderService), typeof(OrderService));
        serviceProvider.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
        serviceProvider.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        serviceProvider.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceProvider.AddAutoMapper(typeof(MapProfile));
        serviceProvider.AddSingleton<CacheDataImportService>();
        serviceProvider.AddHttpClient("IdeaSoftAPI", x =>
        {
            x.BaseAddress = new Uri("https://formaram.myideasoft.com/");
        });
        serviceProvider.AddHttpClient("LogoTransferAPI", x =>
        {
            x.BaseAddress = new Uri("http://89.19.7.130:81/api/api/");
        });

        serviceProvider.AddDbContext<AppDbContext>(x =>
        {
            x.UseSqlServer("Server=94.73.144.17; Database=u8952596_LogoIN; User Id=u8952596_LogoUS; Password=v9P@z7b_W:=jG39U; TrustServerCertificate=True", options =>
            {
                options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
                options.EnableRetryOnFailure();
            });
        });

        var appServices = serviceProvider.BuildServiceProvider();

        await appServices.GetService<CacheDataImportService>().StartAsync();
        await appServices.GetService<IImportService>().SaveOrdersAsync();

        //_timer = new System.Timers.Timer(1 * 5 * 1000);
        //_timer.Enabled = true;
        //_timer.Elapsed += new System.Timers.ElapsedEventHandler(appServices.GetService<IImportService>().StartSync);

        //_timer = new Timer(appServices.GetService<IImportService>().StartSync, 5, 0, 2000);
        //Thread.Sleep(10000);

        Console.ReadLine();
    }
}
