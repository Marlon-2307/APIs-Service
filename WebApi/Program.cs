
using Athenas.EjemploTemplateApi.Application;
using Athenas.EjemploTemplateApi.WebApi.Extensions;
using Athenas.EjemploTemplateApi.WebApi.Settings;


var builder = WebApplication.CreateBuilder(args);


//Agregar para cargar AppConfiguraion
//builder.ConfigureAppConfiguration();


ArgumentNullException.ThrowIfNull(builder.Services);
// Add services to the container.



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Host.UseSerilogWithCustomConfiguration();

string connectionString = builder.Configuration.GetConnectionString("SqlConnection");

builder.Services.AddSqlDataBase(connectionString);
builder.Services.AddUseCases();
builder.Services.AddRepositories();

builder.Services.AddSwaggerConfiguration();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
                       builder.
                       AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseDeveloperExceptionPage();
}

app.UseSwaggerMiddleware();

app.UseHttpsRedirection();

app.MapControllers();

app.UseMiddleware(typeof(ErrorHandlerMiddleware));
app.UseRouting();
app.UseAuthorization();

//Agrega healthcheck Middleware
//app.UseHealthChecksMiddleware();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
