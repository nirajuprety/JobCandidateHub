using AutoMapper;
using JobCandidateHub.API.Services;
using JobCandidateHub.Application;
using JobCandidateHub.Application.Mapper;
using JobCandidateHub.Infrastructure;
using JobCandidateHub.Infrastructure.Mapper;
using JobCandidateHub.Infrastructure.Repository;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<InputValidationFilter>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

var mapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile(new MapperProfile());
    // Add additional mappings as needed
});

IMapper mapper = mapperConfig.CreateMapper();
MapperHelper.Configure(mapper);
builder.Services.AddSingleton(mapper);
var app = builder.Build();
JobCandidateHubDbMigration.UpdateDatabase(app);
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
