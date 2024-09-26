using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Data;
using System.ComponentModel;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MyBudgetManagerAPI;

var builder = WebApplication.CreateBuilder(args);
// Load JWT settings
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"Using connection string: {connectionString}");


// Configure Entity Framework to use a SQL Server Database
builder.Services.AddDbContext<cl_MyBudgetManagerApiDbContext>(options =>
                                                             options.UseSqlServer(connectionString)
                                                                     .EnableSensitiveDataLogging() // Shows detailed query parameters in logs
                                                                     .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                                                             );


// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

// Add Swagger services with custom configuration
builder.Services.AddSwaggerGen(c =>
{
    // This section defines how Swagger groups are named
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MyBudgetManager API",
        Version = "v1"
    });

    // Custom naming for controller groups
    c.TagActionsBy(api =>
    {
        // Here, you can specify custom logic to group actions under custom names
        return [api.GroupName ?? api.ActionDescriptor.RouteValues["controller"]];
    });

    // Optional: Remove the default grouping by controllers if you want a clean structure
    c.DocInclusionPredicate((name, api) => true);

    // Change how schema (model) IDs are generated
    c.CustomSchemaIds(x => x.GetCustomAttributes(false)
        .OfType<DisplayNameAttribute>()
        .FirstOrDefault()?.DisplayName ?? x.Name);

    // Corrects the DateOnly serialisation problem
    c.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date"
    });
});

// Add JWT authentication service
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
        ValidateLifetime = true, // Validate the token expiration
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"])),
        // Set the clock skew to zero to eliminate delay in token expiration
        ClockSkew = TimeSpan.Zero
    };
});

// Configure logging
builder.Logging.AddConsole(); // Enables logging to the console
builder.Logging.SetMinimumLevel(LogLevel.Information); // Set the logging level

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Use the cl_RequireHttps before other middlewares
    app.UseMiddleware<cl_RequireHttps>();
}

// Apply migrations automatically
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<cl_MyBudgetManagerApiDbContext>();

    // Check if the database already exists
    if (!dbContext.Database.CanConnect())
    {
        // If the database doesn't exist, create it and apply migrations
        dbContext.Database.Migrate();
    }
    else
    {
        // Database exists, check for schema updates
        dbContext.Database.EnsureCreated();
    }
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();