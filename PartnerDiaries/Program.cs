using PartnerDiaries;
/*
#####################
        test Area
#####################
 */


/*
#######################
        Main Programm
#######################
 */

//main Program


 var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseUrls( "https://0.0.0.0:8080"); // 

builder.Services.AddSingleton<ProtocolService>(provider =>
{
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    var filePath = Path.Combine(env.WebRootPath, "protocolls.json");
    return new ProtocolService(filePath);
});
builder.Services.AddSingleton<MessageService>(provider =>
{
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    var filePath = Path.Combine(env.WebRootPath, "Messages.json");
    return new MessageService(filePath);
});
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    //options.IdleTimeout = TimeSpan.FromSeconds();
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();
app.MapDefaultControllerRoute();
//var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
//app.Run($"https://0.0.0.0:{port}");
app.Run();