using Ao.Lang;
using Ao.Lang.Runtime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLang(x =>
{
    x.Default = "zh-hans";
    x.LanguageManager.LangService.EnsureGetLangNode("zh-hans")
    .AddInMemoryCollection(new Dictionary<string, string>
    {
        ["hello"] = "ÄãºÃ"
    });
    x.LanguageManager.LangService.EnsureGetLangNode("en-us")
    .AddInMemoryCollection(new Dictionary<string, string>
    {
        ["hello"] = "Hello"
    });
});
builder.Services.AddControllers();

var app = builder.Build();
app.UseRouting();
app.UseEndpoints(x =>
{
    x.MapControllerRoute("default", "{lang?}/{controller=Home}/{action=Index}/{id?}");
});

app.Run();
