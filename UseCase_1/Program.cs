using UseCase_1;
using UseCase_1.Options;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CountriesOptions>(builder.Configuration.GetSection(nameof(CountriesOptions)));

var apiOptions = builder.Configuration.GetSection(nameof(CountriesOptions)).Get<CountriesOptions>();
builder.Services.AddHttpClient(apiOptions!.ApiName)
    .ConfigureHttpClient(httpClient =>
    {
        httpClient.BaseAddress = new Uri(apiOptions.BaseUrl);
    });

builder.Services.AddScoped<ICountriesService, CountriesService>();

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
