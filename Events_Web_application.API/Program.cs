using System.Text;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Events_Web_application.API.MidleWare;
using Events_Web_application.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Events_Web_application.Infrastructure.DBContext;
using Events_Web_application.Application.Services.DBServices;
using Events_Web_application.Application.Services.UnitOfWork;
using Events_Web_application.API.MidleWare.CancellationTokenMidleware;
using Events_Web_application.API.MidleWare.MappingModels.MappingProfiles;
using Events_Web_application.Application.Services.DBServices.DBServicesGenerics;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EWADBContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("MainDBConnection"), b => b.MigrationsAssembly("Events_Web_application.API")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                            ValidateAudience = true,
                            ValidAudience = builder.Configuration["JWT:ValidAudience"],
                            ValidateLifetime = true,
                            RequireExpirationTime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
                            ValidateIssuerSigningKey = true,
                        };
                    });

builder.Services.AddControllersWithViews();

builder.Services.AddControllers(opt => opt.Filters.Add<TaskcanceledExceptionFilter>());


///Add AutoMapper
builder.Services.AddAutoMapper(typeof(EventMappingProfile));




//AddFluentValidation
builder.Services.AddMvc()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

///Add Validation

builder.Services.AddScoped<IDBService<Event>, EventsService>();

builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = "redis:3002";
    options.InstanceName = "redis";
});


//Add Response Compression
builder.Services.AddResponseCompression(options => 
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
    //options.Providers.Add<BrotliCompressionProvider>();

});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.SmallestSize;
});

//builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
//{
//    options.Level = System.IO.Compression.CompressionLevel.SmallestSize;
//});

var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseExceptionHandlerMiddleware();
app.UseResponseCompression();

///CancellationMidlware

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Home}/{action=Index}/{id?}");

app.UseCors( x => 
{
    x.WithHeaders().AllowAnyHeader();
    x.WithOrigins("http://localhost:3000");
    x.WithMethods().AllowAnyMethod();
});
app.Run();
