using MinimalApiDemo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IRepository<Dragon>, DragonRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API V1"));

app.UseCors(p =>
{
    p.AllowAnyOrigin();
    p.AllowAnyMethod();
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/", () => "Hello World!");

app.MapGet("/Hello", (string name) => $"Hello {name}!");

app.MapGet("/Dragons", (IRepository<Dragon> drepo) => drepo.GetAll());

app.MapGet("/Dragons/{id}", (HttpContext http, int id, IRepository<Dragon> drepo) =>
{
    var d = drepo.GetById(id);

    http.Response.StatusCode = d != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound;

    return d;
});

app.MapPost("/Dragons", (HttpContext http, Dragon newDragon, IRepository<Dragon> drepo) =>
{
    drepo.Add(newDragon);
    http.Response.StatusCode = StatusCodes.Status201Created;
});

app.MapPut("/Dragons", (HttpContext http, Dragon updateDragon, IRepository<Dragon> drepo) =>
{
    drepo.Update(updateDragon);
    http.Response.StatusCode = StatusCodes.Status204NoContent;
});

app.MapDelete("/Dragons/{id}", (int id, HttpContext http, IRepository<Dragon> drepo) =>
{
    drepo.Delete(id);
    http.Response.StatusCode = StatusCodes.Status204NoContent;
});

app.Run();
