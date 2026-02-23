using KeduPayments.Application.Abstractions;
using KeduPayments.Application.Interfaces;
using KeduPayments.Application.Mappings;
using KeduPayments.Application.Services;
using KeduPayments.Domain.Interfaces;
using KeduPayments.Infrastructure.Context;
using KeduPayments.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    var cs = builder.Configuration.GetConnectionString("Default");
    opt.UseNpgsql(cs);
});

builder.Services.AddScoped<ICentroCustoRepository, CentroCustoRepository>();
builder.Services.AddScoped<IFinanceiroRepository, FinanceiroRepository>();
builder.Services.AddScoped<IResponsavelFinanceiroRepository, ResponsavelFinanceiroRepository>();
builder.Services.AddScoped<ICentroCustoService, CentroCustoService>();
builder.Services.AddScoped<IFinanceiroService, FinanceiroService>();    
builder.Services.AddScoped<IResponsavelFinanceiroService, ResponsavelFinanceiroService>();
builder.Services.AddSingleton<IPaymentCodeGenerator, PaymentCodeGenerator>();
builder.Services.AddAutoMapper(config => config.AddProfile<DomainToDTOMappingProfile>());

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, _) =>
    {
        document.Info = new()
        {
            Title = "Kedu Planos de Pagamentos",
            Version = "v1",
            Description = "API para gerenciamento de planos de pagamentos.",            

        };
        return Task.CompletedTask;
    }
    );
});

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.MapControllers();

app.Run();
