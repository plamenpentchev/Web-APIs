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
            cnfg.WithOrigins(builder.Configuration["AllowedOrigins"] ?? "*");
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/error/test", (ctxt) =>
{
    var e = new Exception($"Test: {ctxt.User.Identity?.Name}");
    throw e;
});
app.MapGet("/error", () => Results.Problem());

app.MapControllers();

app.Run();
