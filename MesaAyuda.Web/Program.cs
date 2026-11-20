using MesaAyuda.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<
    MesaAyuda.Web.Services.ITicketService,
    MesaAyuda.Web.Services.TicketService>();

var app = builder.Build();


var connectionString = builder.Configuration
    .GetConnectionString("MySqlConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "No se configuró la conexión a MySQL.");
}

try
{
    await using var connection =
        new MySqlConnector.MySqlConnection(connectionString);

    await connection.OpenAsync();

    Console.WriteLine("Conexión a MySQL exitosa.");
}
catch (MySqlConnector.MySqlException ex)
{
    Console.WriteLine(
        $"No se pudo conectar a MySQL. Código: {ex.Number}");
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
