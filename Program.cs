var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.Use(async (ctx, next) =>
{
    // ctx.Response.Headers.Append("X-Frame-Options", "SAMEORIGIN");   
    ctx.Response.Headers.Append("Pragma", "no-cache");
    ctx.Response.Headers.Append("Cache-Control", "max-age=0, no-cache, no-store, must-revalidate");
    ctx.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    ctx.Response.Headers.Append("Access-Control-Allow-Origin", "*");
    await next();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapControllerRoute(
    name: "cat-list",
    pattern: "cat-list/{controller}/{action})",
    defaults: new { controller = "CatList", action = "Index" }
).WithStaticAssets();


app.Run();
