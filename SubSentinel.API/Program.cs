using SubSentinel.API.Data; 
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SubSentinel.API.Data.AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers(); 
builder.Services.AddHostedService<SubSentinel.API.Services.SubscriptionRenewalService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(); 

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers(); 

app.Run();