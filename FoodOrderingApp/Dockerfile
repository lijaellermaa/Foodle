FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 8080

# copy csproj and restore as distinct layers
COPY *.props .
COPY *.sln .

COPY App.BLL/*.csproj ./App.BLL/
COPY App.BLL.DTO/*.csproj ./App.BLL.DTO/
COPY Base.BLL/*.csproj ./Base.BLL/
COPY App.Contracts.BLL/*.csproj ./App.Contracts.BLL/
COPY Base.Contracts.BLL/*.csproj ./Base.Contracts.BLL/

COPY App.Contracts.DAL/*.csproj ./App.Contracts.DAL/
COPY Base.Contracts.DAL/*.csproj ./Base.Contracts.DAL/
COPY App.DAL.DTO/*.csproj ./App.DAL.DTO/
COPY App.DAL.EF/*.csproj ./App.DAL.EF/
COPY Base.DAL/*.csproj ./Base.DAL/
COPY Base.DAL.EF/*.csproj ./Base.DAL.EF/

COPY Base.Contracts.Domain/*.csproj ./Base.Contracts.Domain/
COPY Base.Contracts/*.csproj ./Base.Contracts/
COPY App.Domain/*.csproj ./App.Domain/
COPY Base.Domain/*.csproj ./Base.Domain/

COPY Public.DTO/*.csproj ./Public.DTO/
COPY Helpers.Base/*.csproj ./Helpers.Base/

COPY Tests/*.csproj ./Tests/
COPY WebApp/*.csproj ./WebApp/

RUN dotnet restore

# copy everything else and build app
COPY App.BLL/. ./App.BLL/
COPY App.BLL.DTO/. ./App.BLL.DTO/
COPY Base.BLL/. ./Base.BLL/
COPY App.Contracts.BLL/. ./App.Contracts.BLL/
COPY Base.Contracts.BLL/. ./Base.Contracts.BLL/

COPY App.Contracts.DAL/. ./App.Contracts.DAL/
COPY Base.Contracts.DAL/. ./Base.Contracts.DAL/
COPY App.DAL.DTO/. ./App.DAL.DTO/
COPY App.DAL.EF/. ./App.DAL.EF/
COPY Base.DAL/. ./Base.DAL/
COPY Base.DAL.EF/. ./Base.DAL.EF/

COPY Base.Contracts.Domain/. ./Base.Contracts.Domain/
COPY Base.Contracts/. ./Base.Contracts/
COPY App.Domain/. ./App.Domain/
COPY Base.Domain/. ./Base.Domain/

COPY Public.DTO/. ./Public.DTO/
COPY Helpers.Base/. ./Helpers.Base/

COPY Tests/. ./Tests/
COPY WebApp/. ./WebApp/

RUN dotnet test Tests

WORKDIR /app/WebApp
RUN dotnet publish -c Release -o out

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 8080

COPY --from=build /app/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]