using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Behaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            Services.AddMediatR(Assembly.GetExecutingAssembly());

            Services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehaviour<,>));
            Services.AddTransient(typeof(IPipelineBehavior<,>),typeof(UnHandledException<,>));
            return Services;
        }
    }
}
