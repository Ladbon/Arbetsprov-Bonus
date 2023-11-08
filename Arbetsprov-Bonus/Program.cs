using Arbetsprov_Bonus.Data;
using Arbetsprov_Bonus.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GisysDbContext>(options => options.UseInMemoryDatabase("gisys"));
builder.Services.AddScoped<IConsultantRepository, ConsultantRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:44460") // Replace with your Angular app's URL
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GisysArbetsprovAPI", Version = "v1" });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GisysDbContext>();
    AddTestData(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GisysArbetsprovAPI V1"));

}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseCors();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();

static void AddTestData(GisysDbContext context)
{
    var consultantOne = new Consultant("Money", "Penny", DateTime.UtcNow.AddYears(-1), 0);
    var consultantTwo = new Consultant("Gold", "Finger", DateTime.UtcNow.AddYears(-3), 0);

    context.Consultants.Add(consultantOne);
    context.Consultants.Add(consultantTwo);

    context.SaveChanges();
}