using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.FormWidgets;
using Renderer.Models.GmailWidget;
using Renderer.Services.GoogleServices;
using Renderer.Services.GoogleServices.Authnetication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSitefinity();
builder.Services.AddViewComponentModels();
builder.Services.AddFormViewComponentModels();
builder.Services.AddScoped<IGmailWidgetModel, GmailWidgetModel>();
builder.Services.AddScoped<IGmailPostman, GmailPostman>();
builder.Services.AddScoped<IGoogleAuthentication, GoogleAuthentication>();
builder.Services.AddScoped<IGoogleContacts, GoogleContacts>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSitefinity();

app.UseEndpoints(endpoints =>
{
    endpoints.MapSitefinityEndpoints();
});

app.Run();