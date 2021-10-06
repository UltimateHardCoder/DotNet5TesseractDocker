FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN apt update
RUN apt install -y tesseract-ocr
RUN apt install -y libtesseract-dev
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ORC.csproj", "./"]
RUN dotnet restore "ORC.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "ORC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ORC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ORC.dll"]
