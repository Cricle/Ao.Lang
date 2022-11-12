using Ao.Lang;
using Ao.Lang.Runtime;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLangLocalization();
builder.Services.AddMvc().AddLangMvcLocalization();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddLang(x =>
{
    x.Default = "zh-hans";
    x.LanguageManager.LangService.EnsureGetLangNode("zh-hans")
    .AddInMemoryCollection(new Dictionary<string, string>
    {
        ["Ao.Lang.Asp.Controller.HelloController:hello"] = "ÄãºÃ",
        ["Ao.Lang.Asp.Pages.IndexModel:hello"] = "ÄãºÃ"
    });
    x.LanguageManager.LangService.EnsureGetLangNode("en-us")
    .AddInMemoryCollection(new Dictionary<string, string>
    {
        ["Ao.Lang.Asp.Controller.HelloController:hello"] = "hello",
        ["Ao.Lang.Asp.Pages.IndexModel:hello"] = "hello"
    });
});
builder.Services.AddControllers();

var app = builder.Build();
app.UseRouting();
app.UseRequestLocalization();
app.UseEndpoints(x =>
{
    x.MapControllerRoute("default", "{lang?}/{controller=Home}/{action=Index}/{id?}");
    x.MapRazorPages();
});

app.Run();
