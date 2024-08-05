using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Events_Web_application.Infrastructure.DBContext;
using Events_Web_application.Application.Services.UnitOfWork;
using System.Text;
using FluentValidation.AspNetCore;
using Events_Web_application.API.MidleWare.CancellationTokenMidleware;
using Events_Web_application.API.MidleWare.MappingModels.MappingProfiles;
using Events_Web_application.API.MidleWare;
using System.Reflection;
using Events_Web_application.Domain.Entities;
using FluentValidation;
using Events_Web_application.Application.Services.Validation;
using Events_Web_application.Application.Services.DBServices.DBServicesGenerics;
using Events_Web_application.Application.Services.DBServices;

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
    options.Configuration = "localhost:3002";
    options.InstanceName = "agitated_bohr";
});

//Add React
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseExceptionHandlerMiddleware();

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
