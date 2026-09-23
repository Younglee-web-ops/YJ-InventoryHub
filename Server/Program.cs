var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddCors();

var app = builder.Build();

// 1. 오류 처리 미들웨어 (가장 먼저)
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"error\": \"Internal server error.\"}");
    }
});

// 2. 인증 미들웨어 (두 번째) - /api/data/inventory, /api/productlist는 인증 제외
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/api/data/inventory") ||
        context.Request.Path.StartsWithSegments("/api/productlist"))
    {
        await next();
        return;
    }

    var token = context.Request.Headers["Authorization"].ToString();
    if (string.IsNullOrWhiteSpace(token) || token != "Bearer mysecrettoken")
    {
        context.Response.StatusCode = 401;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"error\": \"Unauthorized. Valid token required.\"}");
        return;
    }
    await next();
});

// 3. 로깅 미들웨어 (마지막)
app.Use(async (context, next) =>
{
    Console.WriteLine($"[{DateTime.Now}] {context.Request.Method} {context.Request.Path}");
    await next();
    Console.WriteLine($"[{DateTime.Now}] Response: {context.Response.StatusCode}");
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.MapControllers();

app.Run();