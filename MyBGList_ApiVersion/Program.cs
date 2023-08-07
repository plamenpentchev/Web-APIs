using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyBGList_ApiVersion.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBGList", Version = "v1.0" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "MyBGList", Version = "v2.0" });
    options.SwaggerDoc("v3", new OpenApiInfo { Title = "MyBGList", Version = "v3.0" });
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(cnfg =>
    {
        var ao = builder.Configuration["AllowedOrigins"]?.Split(";") ?? new[] { "*" };
        cnfg.WithOrigins(origins: ao);
        cnfg.AllowAnyHeader();
        cnfg.AllowAnyMethod();
    });
    options.AddPolicy(name: "AnyOrigin", cnfg =>
    {
        cnfg.AllowAnyOrigin();
        cnfg.AllowAnyHeader();
        cnfg.AllowAnyMethod();
    });
    options.AddPolicy(name: "AnyOrigin_GetOnly", cnfg =>
    {
        cnfg?.AllowAnyOrigin();
        cnfg?.AllowAnyHeader();
        cnfg?.WithMethods(HttpMethods.Get);
    });
}
);// Versioning
builder.Services.AddApiVersioning( options =>
{
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddDbContext<ApplicationDbContext>( options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultCOnnection") ?? "");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/v1/swagger.json", "MyBGList v1");
        options.SwaggerEndpoint($"/swagger/v2/swagger.json", "MyBGList v2");
        options.SwaggerEndpoint($"/swagger/v3/swagger.json", "MyBGList v3");
    });
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/v{version:ApiVersion}/error/test",
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ResponseCache(NoStore = true)]
    [EnableCors(PolicyName = "AnyOrigin")] (ctxt) =>
    {
        var e = new Exception($"Test: {ctxt.User.Identity?.Name}");
        throw e;
    });
app.MapGet("/v{version:ApiVersion}/error",
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ResponseCache(NoStore = true)]
    [EnableCors(PolicyName = "AnyOrigin")] () => Results.Problem());

app.Map("/v{version:ApiVersion}/cod/test",
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [EnableCors(PolicyName = "AnyOrigin_GetOnly")]
    [ResponseCache(NoStore = true)]
    () =>
    {
        return Results.Text("<script>" +
             "window.alert('Your client supports JavaScript!" +
             "\\r\\n\\r\\n" +
             $"Server time (UTC): {DateTime.UtcNow.ToString("o")}" +
             "\\r\\n" +
             "Client time (UTC): ' + new Date().toISOString());" +
             "</script>" +
             "<noscript>Your client does not support JavaScript</noscript>",
             "text/html");
    });

app.MapControllers();

app.Run();
