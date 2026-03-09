using Microsoft.EntityFrameworkCore;
using StorageManagement.Data;
using StorageManagement.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

var singleUserId = "";

//Check is StorageManagement.db is exists
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    //check it user is not present then only create
    var user = db.Users.FirstOrDefault(u => u.Username == "Dev6sh");
    if (user == null)
    {
        user = new User
        {
            Username = "Dev6sh",
            Password = "AndroDev@Passwords",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        db.Users.Add(user);
        db.SaveChanges();
    }
    singleUserId = user.Id.ToString();
}

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.MapGet("/", () => "Hello World!");

app.MapGet("/add-password", (string name, string username, string password, string accessKey, string url="", string notes="") => {
    //first check accessKey is correct or not
    if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
    {
        return Results.Json(new {message = "Invalid data"});
    }
    else if(accessKey != "AndroDev@Passwords" && username != "Dev6sh")
    {
        return Results.Json(new {message = "Invalid access key"});
    }
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var newData = new Password
        {
            Name = name,
            Username = username,
            PasswordHash = password,
            Url = url,
            Notes = notes,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        db.Passwords.Add(newData);
        db.SaveChanges();
    }
    return Results.Json(new {message = "Password added successfully"});
});

app.MapGet("/get-passwords", (string accessKey,string username,string name) => {
    //first check accessKey is correct or not
    if(accessKey != "AndroDev@Passwords" && username != "Dev6sh")
    {
        return Results.Json(new {message = "Invalid access key"});
    }
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var passwords = db.Passwords.Where(p => p.Name == name && p.Username == username).ToList();
        return Results.Json(passwords);
    }
});


// app.UseAuthorization();

// app.MapStaticAssets();
// app.MapRazorPages()
//    .WithStaticAssets();

app.Run();
