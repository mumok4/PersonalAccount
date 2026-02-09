# Образ
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR app

# Копируем
COPY . .

# Собираем
RUN dotnet build PersonalAccount.sln
RUN dotnet test PersonalAccount.sln
RUN dotnet publish --output ./publish PersonalAccount.sln 

# Запускаем
WORKDIR publish
ENTRYPOINT ["dotnet", "PersonalAccount.Console.dll"]


