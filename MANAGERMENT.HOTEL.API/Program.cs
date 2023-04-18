
using MANAGERMENT.HOTEL.BL.BaseBL;
using MANAGERMENT.HOTEL.BL.Service.RoomService;
using MANAGERMENT.HOTEL.Common.Entities.Model;
using MANAGERMENT.HOTEL.DL;
using MANAGERMENT.HOTEL.DL.BaseDL;
using MANAGERMENT.HOTEL.DL.Service.RoomService;
using MANAGERMENT.HOTEL.DL.Service.StatusService;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:7065")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

DatabaseContext.ConnectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped<IRoomDL, RoomDL>();
builder.Services.AddScoped<IStatusDL, StatusDL>();
builder.Services.AddScoped<IRoomBL, RoomBL>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("MyPolicy");

app.MapControllers();

app.Run();
