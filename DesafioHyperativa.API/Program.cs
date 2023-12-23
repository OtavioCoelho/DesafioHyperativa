using DesafioHyperativa.API.Extensions;
using DesafioHyperativa.API.Filters;
using DesafioHyperativa.API.Infra;
using DesafioHyperativa.Domain.Contracts.CrossCutting;
using DesafioHyperativa.Domain.Contracts.Services;
using DesafioHyperativa.Repository.CrossCutting;
using DesafioHyperativa.Repository.Extensions;
using DesafioHyperativa.Service.Extensions;
using System.Net.Mime;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
}).AddControllersAsServices();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRepository();
builder.Services.AddService();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSeriLog(builder);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.ConfigSwagger();
builder.Services.ConfigSecurity();

builder.Services.AddHyperativaDbContext(
    builder.Configuration,
    builder.Environment.IsDevelopment());

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    {
        return RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(), partition =>
            new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                AutoReplenishment = true,
                Window = TimeSpan.FromSeconds(30)
            });
    });

    options.OnRejected = async (context, token) =>
    {
        var response = context.HttpContext.Response;
        response.StatusCode = StatusCodes.Status429TooManyRequests;
        response.ContentType = MediaTypeNames.Application.Json;
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        CustomModelError error = new();
        string mensage;

        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
            mensage = $"Too many requests sent. Please try again after {string.Format("{0:N2}", retryAfter.TotalSeconds)} second(s).";
        else
            mensage = "Too many requests sent. Please try again later.";

        error = new CustomModelError()
        {
            Title = "Too Many Requests",
            Status = "429",
            Errors = mensage
        };
        await response.WriteAsJsonAsync(error);
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.ConfigUISwagger();
}

app.ConfigSecurity();

app.ConfigMiddleware();

app.UseRouting();

app.UseHttpsRedirection();

app.ConfigSerilog();

app.UseAuthorization();

app.MapControllers();

app.UseRateLimiter();

app.Run();
