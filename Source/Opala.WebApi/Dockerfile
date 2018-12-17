FROM microsoft/dotnet:2.1-aspnetcore-runtime
ARG source
WORKDIR /app
EXPOSE 80
COPY . .
ENTRYPOINT ["dotnet", "Opala.WebApi.dll"]