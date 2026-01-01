using Credit_Wallet.Data;
using Credit_Wallet.Features.MakeWallet;
using Credit_Wallet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Credit_Wallet.Features.GetuserWallet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IMakeWalletService, MakeWalletService>();
builder.Services.AddScoped<GetUserWalletHandler>();

var app = builder.Build();
GetUserWalletEndpoint.MapGetUserWalletEndpoint(app);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
   // app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
