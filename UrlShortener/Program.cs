using UrlShortener.Database;
using UrlShortener.Options;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MachineOptions>(
    builder.Configuration.GetSection(MachineOptions.Machine));
builder.Services.AddScoped<IIdEncoder, Base62IdEncoder>();
// Option 1: 100% in-memory implementation
//builder.Services.AddScoped<IUrlService, InMemoryUrlService>();
// Option 2: In-memory EF implementation
builder.Services.AddDbContext<UrlsContext>();
builder.Services.AddSingleton<IDateTime, CurrentDateTime>();
builder.Services.AddSingleton<IIdGenerator, SnowflakeIdGenerator>();
builder.Services.AddScoped<IUrlService, DbBasedUrlService>();

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
