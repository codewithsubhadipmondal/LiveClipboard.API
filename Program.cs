using LiveClipboard.API.Hubs;
using LiveClipboard.API.Interfaces;
using LiveClipboard.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Enable CORS for Angular frontend
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.AllowAnyHeader()
			  .AllowAnyMethod()
			  .SetIsOriginAllowed(_ => true)
			  .AllowCredentials();
	});
});

// Add SignalR and services
builder.Services.AddSignalR();
builder.Services.AddSingleton<IClipboardService, ClipboardService>();

builder.Services.AddControllers(); // For REST APIs
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Swagger UI

var app = builder.Build();

app.UseCors();
app.UseRouting();

app.UseAuthorization();
app.MapControllers(); // Enable API Controllers

// SignalR Hub endpoint
app.MapHub<ClipboardHub>("/clipboardHub");

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
