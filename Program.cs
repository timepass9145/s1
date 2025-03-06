//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using SIBSAPI.Data;
//using SIBSAPI.Services;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// ✅ Configure database connection
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// ✅ Configure JWT Authentication
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//    };
//});

//// ✅ Register Application Services
//builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
//builder.Services.AddScoped<IEmployee, EmployeeService>();

//// ✅ Add Controllers
//builder.Services.AddControllers();

//// ✅ Add CORS (Optional, use if needed)
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//        policy => policy.AllowAnyOrigin()
//                        .AllowAnyMethod()
//                        .AllowAnyHeader());
//});

//// ✅ Configure Swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "SIBSAPI",
//        Version = "v1",
//        Description = "API for SIBS Project",
//    });

//    // Add JWT Support in Swagger
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "Enter 'Bearer {your token}'",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.Http,
//        Scheme = "bearer"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
//            },
//            new string[] { }
//        }
//    });
//});

//var app = builder.Build();

//// ✅ Ensure Swagger works correctly
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SIBSAPI v1");
//        c.RoutePrefix = "swagger";  // Ensure it's accessible at /swagger
//    });
//}

//// ✅ Enable CORS (Optional)
//app.UseCors("AllowAll");

//// ✅ Redirect root URL to Swagger UI (Optional)
//app.Use(async (context, next) =>
//{
//    if (context.Request.Path == "/")
//    {
//        context.Response.Redirect("/swagger");
//        return;
//    }
//    await next();
//});

//// ✅ Enforce HTTPS
//app.UseHttpsRedirection();

//// ✅ Enable Authentication & Authorization
//app.UseAuthentication();
//app.UseAuthorization();

//// ✅ Map API Controllers
//app.MapControllers();

//// ✅ Run the Application
//app.Run();

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SIBSAPI.Data;
using SIBSAPI.Services;
using System.Text;

//var reactAppUrl = new Uri("http://192.168.1.7:3000");  //new Uri("http://localhost:3000");
var builder = WebApplication.CreateBuilder(args);
////////////////////
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Database connection string is missing or empty.");
}
////////////////////////
builder.WebHost.UseUrls("http://localhost:5191", "https://localhost:7189");

// ✅ Configure database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ✅ Null check before using JWT configuration values
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key", "Jwt:Key is missing in configuration.");
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer", "Jwt:Issuer is missing.");
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience", "Jwt:Audience is missing.");



// ✅ Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// ✅ Register Application Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// ✅ Add Controllers
builder.Services.AddControllers();

// ✅ Add CORS to allow React frontend requests
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000") // Replace with your React frontend URL if needed
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// ✅ Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SIBSAPI",
        Version = "v1",
        Description = "API for SIBS Project",
    });

    // Add JWT Support in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer {your token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] { }
        }
    });
});



var app = builder.Build();

// ✅ Ensure Swagger works correctly
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SIBSAPI v1");
//        c.RoutePrefix = "swagger";  // Ensure it's accessible at /swagger
//    });
//}


//✅ Ensure Swagger works correctly
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
     {
         c.SwaggerEndpoint("/swagger/v1/swagger.json", "SIBSAPI v1");
         c.RoutePrefix = "swagger";  // Ensure it's accessible at /swagger
     });
//}






    // ✅ Enable CORS
    app.UseCors("AllowReactApp");

// ✅ Redirect root URL to Swagger UI (Optional)
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});

// ✅ Enforce HTTPS
app.UseHttpsRedirection();

// ✅ Enable Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

//builder.WebHost.UseUrls("http://localhost:5191;https://localhost:7189");

// ✅ Map API Controllers
app.MapControllers();

// ✅ Run the Application
app.Run();

