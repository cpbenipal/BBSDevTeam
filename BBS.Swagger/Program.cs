using BBS.Entities;
using BBS.Swagger.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using NLog; 

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BusraDbContext>(options => options.UseNpgsql(connectionString));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

/*
services.Configure<SendGridEmailSenderOptions>(options =>
{
    options.ApiKey = Configuration["ExternalProviders:SendGrid:ApiKey"];
    options.SenderEMail = Configuration["ExternalProviders:SendGrid:SenderEmail"]; 
    options.SenderName = Configuration["ExternalProviders:SendGrid:SenderName"];
});
*/
builder.Services.ConfigureJWTToken();
builder.Services.ConfigureRepositoryWrapper();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers(); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();    
    app.UseDeveloperExceptionPage();
}
else
    app.UseHsts();


app.UseRouting();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
}); 

app.Run();
