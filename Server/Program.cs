using Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы в контейнер.
builder.Services.AddControllers();

// Добавляем поддержку Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрируем orderRepository
builder.Services.AddSingleton<IModelDB>(provider =>
{
    // Создаем базу данных и передаем путь к ней
    string connectPath = "Data Source=DataBase.db"; 
    // Создаем экземпляр репозитория и передаем путь к базе данных SQLite
    IModelDB orderRepository = new SQLiteRepositiry(connectPath);
    return orderRepository; // Путь к файлу базы данных SQLite
});

var app = builder.Build();

// Настраиваем конвейер обработки HTTP-запросов.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();