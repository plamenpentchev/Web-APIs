using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyBGList;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//... BAD PRACTICE if you want to keep both MapGet(/error,...) and MapCotrollers with ErrorController
//builder.Services.AddSwaggerGen(opts => opts.ResolveConflictingActions(apiDesc => apiDesc.First()) );
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(cnfg =>
        {
            var ao = builder.Configuration["AllowedOrigins"]?.Split(";") ?? new[] { "*" };
            cnfg.WithOrigins( origins: ao );
            cnfg.AllowAnyHeader();
            cnfg.AllowAnyMethod();
        });
        options.AddPolicy(name: "AnyOrigin", cnfg =>
        {
            cnfg.AllowAnyOrigin();
            cnfg.AllowAnyHeader();
            cnfg.AllowAnyMethod();
        });
    }
);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Configuration.GetValue<bool>("UseSwagger"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Configuration.GetValue<bool>("UseDeveloperExceptionPage"))
{
    app.UseDeveloperExceptionPage();

}
else
{
    app.UseExceptionHandler("/error");
}
//..Use CORS by means of CORS middleware
//... applies the selected CORS policy(here default - no name) to all endpoints.
app.UseCors(); // or named policy -> app.USeCors("AnyOrigin")

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapGet("/error/test",
    [ResponseCache(NoStore = true)]
    [EnableCors(PolicyName ="AnyOrigin")] (ctxt) =>
{
    var e = new Exception($"Test: {ctxt.User.Identity?.Name}");
    throw e;
});
app.MapGet("/error",
    [ResponseCache(NoStore =true)]
    [EnableCors(PolicyName = "AnyOrigin")] () => Results.Problem());

app.MapGet("/cod/test",
    [EnableCors]
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
