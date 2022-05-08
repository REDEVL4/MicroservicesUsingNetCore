using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Contracts.Persistence;
using Order.Application.Features.Infastructure;
using Order.Application.Models;
using Order.Infastructure.Mail.EmailService;
using Order.Infastructure.Persistence;
using Order.Infastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infastructure
{
    public static class InfastructureServiceregistration
    {
       public static IServiceCollection InfastructureServices(this IServiceCollection Services,IConfiguration configuration)
        {
            Services.AddDbContext<OrderContext>(c => c.UseSqlServer(configuration.GetValue<string>("Secrets:ConnectionString1")));
            Services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            Services.AddScoped<IOrderRepository,OrderRepository>();
            Services.Configure<EmailSetting>(configuration.GetSection("EmailSetting"));
            Services.AddSingleton<IEmailHandler,EmailService>();
            return Services;
        }
    }
}
