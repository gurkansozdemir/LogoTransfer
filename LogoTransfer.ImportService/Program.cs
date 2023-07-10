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
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IdeaSoftService>();
builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
builder.Services.AddScoped(typeof(IProductService), typeof(ProductService));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddSingleton<CacheData>();


builder.Services.AddHttpClient("IdeaSoftAPI", x =>
{
    x.BaseAddress = new Uri("https://formaram.myideasoft.com/");
});
builder.Services.AddHttpClient("LogoTransferAPI", x =>
{
    x.BaseAddress = new Uri("https://localhost:7096/api/");
});

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer("Server=94.73.144.17; Database=u8952596_LogoIN; User Id=u8952596_LogoUS; Password=v9P@z7b_W:=jG39U; TrustServerCertificate=True", options =>
    {
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
        options.EnableRetryOnFailure();
    });
});

var app = builder.Build();

app.Services.GetService<IdeaSoftService>().SaveOrdersAsync();

app.Run();