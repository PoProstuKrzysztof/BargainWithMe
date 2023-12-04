using BargainWithMe.Core.Contracts.RepositoryContracts;
using BargainWithMe.Core.Contracts.ServiceContracts;
using BargainWithMe.Core.Services;
using BargainWithMe.Infrastructure.Data;
using BargainWithMe.Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding database
builder.Services.AddDbContext<RepositoryContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
sqlServerOptionsAction: sqlOptions =>
{
    sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
}
));

//Depedency injection
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<INegotiationService, NegotiationService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

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