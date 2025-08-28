using GameBlazorApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Create API base link
builder.Services.AddHttpClient("GamesAPI", client =>
{
    client.BaseAddress = new Uri("https://mycademo-akczfsccb9fueehq.westeurope-01.azurewebsites.net/");
});

// Register the API call services
builder.Services.AddScoped<IDeveloperApiService, DeveloperApiService>();
builder.Services.AddScoped<IGameApiService, GameApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
