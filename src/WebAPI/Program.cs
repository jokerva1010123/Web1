using Microsoft.EntityFrameworkCore;
using ApplicationDbContext;
using DataAcess;
using Interface;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<IUserDA, UserDA>();
builder.Services.AddTransient<IRoomDA, RoomDA>();
builder.Services.AddTransient<IStudentDA, StudentDA>();
builder.Services.AddTransient<IThingDA, ThingDA>();
builder.Services.AddTransient<UserServices>();
builder.Services.AddTransient<RoomServices>();
builder.Services.AddTransient<StudentServices>();
builder.Services.AddTransient<ThingServices>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: MyAllowSpecificOrigins, policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(MyAllowSpecificOrigins);
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
