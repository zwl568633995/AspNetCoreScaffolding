FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Kay.Boilerplate.Application.Service.Http/Kay.Boilerplate.Application.Service.Http.csproj", "Kay.Boilerplate.Application.Service.Http/"]
COPY ["Kay.Framework.Redis/Kay.Framework.Redis.csproj", "Kay.Framework.Redis/"]
COPY ["Kay.Framework.Swagger/Kay.Framework.Swagger.csproj", "Kay.Framework.Swagger/"]
COPY ["Kay.Framework.AspNetCore/Kay.Framework.AspNetCore.csproj", "Kay.Framework.AspNetCore/"]
COPY ["Kay.Framework/Kay.Framework.csproj", "Kay.Framework/"]
COPY ["Kay.Framework.Utility/Kay.Framework.Utility.csproj", "Kay.Framework.Utility/"]
COPY ["Kay.Framework.ObjectMapping.TinyMapper/Kay.Framework.ObjectMapping.TinyMapper.csproj", "Kay.Framework.ObjectMapping.TinyMapper/"]
COPY ["Kay.Framework.ObjectMapping.Abstractions/Kay.Framework.ObjectMapping.Abstractions.csproj", "Kay.Framework.ObjectMapping.Abstractions/"]
COPY ["Kay.Framework.AspNetCore.Mvc/Kay.Framework.AspNetCore.Mvc.csproj", "Kay.Framework.AspNetCore.Mvc/"]
COPY ["Kay.Framework.EntityFrameworkCore.SqlServer/Kay.Framework.EntityFrameworkCore.SqlServer.csproj", "Kay.Framework.EntityFrameworkCore.SqlServer/"]
COPY ["Kay.Framework.EntityFrameworkCore.MySQL/Kay.Framework.EntityFrameworkCore.MySQL.csproj", "Kay.Framework.EntityFrameworkCore.MySQL/"]
COPY ["Kay.Framework.EntityFrameworkCore.Oracle/Kay.Framework.EntityFrameworkCore.Oracle.csproj", "Kay.Framework.EntityFrameworkCore.Oracle/"]
COPY ["Kay.Framework.Domain/Kay.Framework.Domain.csproj", "Kay.Framework.Domain/"]
COPY ["Kay.Boilerplate.Infrastructure.BoundedContext.Ef/Kay.Boilerplate.Infrastructure.BoundedContext.Ef.csproj", "Kay.Boilerplate.Infrastructure.BoundedContext.Ef/"]
COPY ["Kay.Boilerplate.Domain/Kay.Boilerplate.Domain.csproj", "Kay.Boilerplate.Domain/"]
COPY ["Kay.Framework.Domain.EntityFrameworkCore/Kay.Framework.Domain.EntityFrameworkCore.csproj", "Kay.Framework.Domain.EntityFrameworkCore/"]
COPY ["Kay.Boilerplate.ApplicationService/Kay.Boilerplate.ApplicationService.csproj", "Kay.Boilerplate.ApplicationService/"]
COPY ["Kay.Framework.Application/Kay.Framework.Application.csproj", "Kay.Framework.Application/"]
COPY ["Kay.Framework.Authorization/Kay.Framework.Authorization.csproj", "Kay.Framework.Authorization/"]
COPY ["Kay.Boilerplate.Infrastructure.Treasure/Kay.Boilerplate.Infrastructure.Treasure.csproj", "Kay.Boilerplate.Infrastructure.Treasure/"]
RUN dotnet restore "Kay.Boilerplate.Application.Service.Http/Kay.Boilerplate.Application.Service.Http.csproj"
COPY . .
WORKDIR "/src/Kay.Boilerplate.Application.Service.Http"
RUN dotnet build "Kay.Boilerplate.Application.Service.Http.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Kay.Boilerplate.Application.Service.Http.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Kay.Boilerplate.Application.Service.Http.dll"]