using Autofac;
using CafeOps.DAL.Repositories.Interfaces;
using CafeOps.DAL.Repositories;
using CafeOps.Logic;
using MediatR;

namespace CafeOps.API.Utils
{
    public class AutofacModule : Module
    {
        //protected override void Load(ContainerBuilder builder)
        //{
        //    // Register MediatR
        //    builder.RegisterType<CafeRepository>().As<ICafeRepository>().InstancePerLifetimeScope();
        //    builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerLifetimeScope();
        //    builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();

        //    // Register all IRequestHandler implementations
        //    builder.RegisterAssemblyTypes(typeof(GetCafesQueryHandler).Assembly)
        //           .AsClosedTypesOf(typeof(IRequestHandler<,>))
        //           .AsImplementedInterfaces();

        //    // Register the ServiceFactory for MediatR
        //    builder.Register<ServiceFactory>(context =>
        //    {
        //        var c = context.Resolve<IComponentContext>();
        //        return t => c.Resolve(t);
        //    });

        //    builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
        //}
    }
}
