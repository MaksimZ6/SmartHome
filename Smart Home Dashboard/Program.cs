using Smart_Home_Dashboard.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies; // Добавлено для куки
using Microsoft.AspNetCore.Components.Authorization; // Добавлено для авторизации

var builder = WebApplication.CreateBuilder(args);

// 1. РЕГИСТРИРУЕМ БАЗУ ДАННЫХ
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=smart_home.db"));

// 2. НАСТРОЙКА АУТЕНТИФИКАЦИИ (КУКИ)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/login"; // Куда перенаправлять неавторизованных
        options.AccessDeniedPath = "/login";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });

// 3. СЛУЖБЫ ДЛЯ ОБРАБОТКИ СОСТОЯНИЯ АВТОРИЗАЦИИ В BLAZOR
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// 4. АВТОМАТИЧЕСКОЕ СОЗДАНИЕ БАЗЫ ПРИ СТАРТЕ
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// 5. ВАЖНО: ПОРЯДОК MIDDLEWARE
app.UseAuthentication(); // Кто ты?
app.UseAuthorization();  // Можно ли тебе сюда?
app.UseAntiforgery();

app.MapRazorComponents<Smart_Home_Dashboard.Components.App>()
    .AddInteractiveServerRenderMode();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    // Если в базе еще нет пользователей, добавляем админа
    if (!db.Users.Any())
    {
        db.Users.Add(new User { Username = "1234", Password = "123" });
        db.SaveChanges();
    }
}

app.Run();
