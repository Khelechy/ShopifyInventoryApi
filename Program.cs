using Microsoft.EntityFrameworkCore;
using ShopifyInventoryApi.Data;
using ShopifyInventoryApi.Interfaces;
using ShopifyInventoryApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InventoryDbContext>(opt => opt.UseInMemoryDatabase("inventoryDB"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IInventoryService, InventoryService>();
builder.Services.AddTransient<IShipmentService, ShipmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();
