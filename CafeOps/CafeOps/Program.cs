using Autofac;
using Autofac.Extensions.DependencyInjection;
using CafeOps.DAL;
using CafeOps.DAL.Repositories.Interfaces;
using CafeOps.DAL.Repositories;
using CafeOps.Logic;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conString = builder.Configuration.GetConnectionString("DefaultConnection") ??
     throw new InvalidOperationException("Connection string 'DefaultConnection'" +
    " not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(conString));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterType<CafeRepository>().As<ICafeRepository>().InstancePerLifetimeScope();
    container.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerLifetimeScope();

    // Register MediatR
    container.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

    // Register all IRequestHandler implementations
    container.RegisterAssemblyTypes(typeof(GetCafesQueryHandler).Assembly)
             .AsClosedTypesOf(typeof(IRequestHandler<,>))
             .AsImplementedInterfaces();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
