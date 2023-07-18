using Microsoft.EntityFrameworkCore;
using RestApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<FeatureFlagDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("LocalDb"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
