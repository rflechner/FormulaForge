using FormulaForge.ApiService.Persistence;
using FormulaForge.ApiService.Persistence.Postgres;
using FormulaForge.Domain.Services;
using FormulaForge.Domain.Services.Impl;
using FormulaForge.Web;
using FormulaForge.Web.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FluentUI.AspNetCore.Components;
using SkiaSharp;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<FormulaForgeDbContext>("formulaforge");
builder.Services.AddScoped<IProjectRepository, PostgresProjectRepository>();
builder.Services.AddScoped<IProjectManagementService, ProjectManagementService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOutputCache();

builder.Services.AddHttpClient();

builder.Services.AddHttpClient<WeatherApiClient>(client =>
{
    // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
    // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
    client.BaseAddress = new("https+http://apiservice");
});

builder.Services.AddFluentUIComponents();

// Add authentication services
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = builder.Configuration["Authentication:PocketId:Authority"];
    options.ClientId = builder.Configuration["Authentication:PocketId:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:PocketId:ClientSecret"];
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");
    options.CallbackPath = "/signin-oidc";
    options.SignedOutCallbackPath = "/signout-callback-oidc";
    options.TokenValidationParameters.NameClaimType = "name";
    options.RequireHttpsMetadata = false;

    // Fix for IDX20803: Authority needs to be reachable by the server, 
    // but metadata might contain localhost URLs that the server can't reach if it's in a container.
    // In Aspire, we use the service endpoint for the server-to-server communication.
    options.Events = new OpenIdConnectEvents
    {
        OnRedirectToIdentityProvider = context =>
        {
            // If we are redirecting the browser, we might need to use the external URL.
            // But since Authority is already set to the service endpoint, 
            // OIDC might use it for redirects too.
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapGet("/logout", () =>
{
    var properties = new AuthenticationProperties { RedirectUri = "/" };
    return Results.SignOut(properties, [ "Cookies", "oidc" ]);
});

app.MapGet("/avatar", async (string url, [FromServices] IHttpClientFactory httpClientFactory, [FromServices] IConfiguration configuration) =>
{
    if (string.IsNullOrEmpty(url)) return Results.BadRequest();

    var authority = configuration["Authentication:PocketId:Authority"];
    if (string.IsNullOrEmpty(authority) || !url.StartsWith(authority, StringComparison.OrdinalIgnoreCase))
    {
        return Results.BadRequest();
    }

    try
    {
        var client = httpClientFactory.CreateClient();
        var response = await client.GetAsync(url);
        if (!response.IsSuccessStatusCode) return Results.BadRequest();

        await using var inputStream = await response.Content.ReadAsStreamAsync();
        using var codec = SKCodec.Create(inputStream);
        if (codec == null) return Results.BadRequest();

        using var bitmap = SKBitmap.Decode(codec);
        if (bitmap == null) return Results.BadRequest();

        int targetWidth = 40;
        int targetHeight = 40;

        // Calculate proportions to match "Max" behavior (contain)
        float ratio = Math.Min((float)targetWidth / bitmap.Width, (float)targetHeight / bitmap.Height);
        int newWidth = (int)(bitmap.Width * ratio);
        int newHeight = (int)(bitmap.Height * ratio);

        using var resized = bitmap.Resize(new SKImageInfo(newWidth, newHeight), SKSamplingOptions.Default);
        if (resized == null) return Results.BadRequest();

        using var image = SKImage.FromBitmap(resized);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        
        var outputStream = new MemoryStream();
        data.SaveTo(outputStream);
        outputStream.Position = 0;

        return Results.File(outputStream, "image/png");
    }
    catch
    {
        return Results.BadRequest();
    }
}).RequireAuthorization();

app.MapPost("/logout", () =>
{
    var properties = new AuthenticationProperties { RedirectUri = "/" };
    return Results.SignOut(properties, [ "Cookies", "oidc" ]);
});

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// The following line might be too restrictive if we want some pages to be anonymous.
// Since Blazor components use [Authorize] or [AllowAnonymous], 
// we can rely on the router and component attributes.
// app.MapGroup("/").RequireAuthorization();

app.MapDefaultEndpoints();

app.Run();