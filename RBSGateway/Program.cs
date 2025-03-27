using Microsoft.EntityFrameworkCore;
using RBSGateway.Data;
using RBSGateway.Interface;
using RBSGateway.Mapping;
using RBSGateway.Repository;
using RBSGateway.Services.ResourceServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DEV")));
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
builder.Services.AddScoped<IResourceNameRepository, ResourceNameRepository>();
builder.Services.AddScoped<IResourceService, ResourceService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
