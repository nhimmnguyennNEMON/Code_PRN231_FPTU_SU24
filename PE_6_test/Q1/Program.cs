using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Q1.DTOs;
using Q1.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<DUONGPV14_PRNContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

ODataConventionModelBuilder mdBuilder = new ODataConventionModelBuilder();
mdBuilder.EntitySet<StudentDTO>("Student");

builder.Services.AddControllers().AddOData(opt => opt.Select().Count().Filter().AddRouteComponents("odata", mdBuilder.GetEdmModel())); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();