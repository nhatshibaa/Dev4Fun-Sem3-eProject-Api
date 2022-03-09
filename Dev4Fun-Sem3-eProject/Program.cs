using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Dev4Fun_Sem3_eProject.Data;
using Dev4Fun_Sem3_eProject.Settings;
using Dev4Fun_Sem3_eProject.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Dev4Fun_Sem3_eProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Dev4Fun_Sem3_eProjectContext")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
