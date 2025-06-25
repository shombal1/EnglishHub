# EnglishHub

Проект сделан по циклу "12-факторное приложение на dotnet": https://www.youtube.com/watch?v=x_CBZ4xMlm0&list=PLdYH-BkSbBMIReRp2rhAhc4gHZVGNXKUh

## Описание проекта

EnglishHub - Форумный движок. Проект построен на микросервисной архитектуре с использованием ASP.NET Core.

## Структура проекта

Проект состоит из следующих компонентов:

- **EnglishHub.Forums.Api**: API для работы с форумами
- **EnglishHub.Forums.Domain**: Бизнес-логика форумов
- **EnglishHub.Forums.Storage**: Доступ к данным форумов
- **EnglishHub.Search.Api**: API для поисковых запросов
- **EnglishHub.Search.Domain**: Бизнес-логика поиска
- **EnglishHub.Search.Storage**: Доступ к данным поиска

## Требования

- .NET 8.0 или выше
- Docker Desktop
- dotnet-ef (Entity Framework Core CLI)

# EnglishHub

Что бы запустить приложение локально нужно установить Docker-Desktop.
После этого нужно выполнить команду в директории репозитория.

```shell
docker compose -f ./docker/docker-compose.yml up -d
```

Для миграции базы данных нужно установть ef
```shell
dotnet tool install --global dotnet-ef
```

Для создания таблиц в базе данных нужно выполнить в директории репозитория команду
```shell
dotnet ef database update  -p Postcrossing.Storage -s Postcrossing.Api
```

