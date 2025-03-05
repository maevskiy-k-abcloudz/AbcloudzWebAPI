var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMediator, Mediator>();

var dbOptions = new DbContextOptionsBuilder<MainContext>()
    .UseInMemoryDatabase(databaseName: "test")
    .Options;
var context = new MainContext(dbOptions);
builder.Services.AddSingleton<MainContext>(x => context);

builder.Services
    .AddCQRS(typeof(Program).Assembly)
    .AddEntityFrameworkIntegration<MainContext>(TransactionBehaviorEnum.ScopeBehavior);

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
