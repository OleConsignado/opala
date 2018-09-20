FROM microsoft/aspnetcore:2.0.0-jessie
ARG source
WORKDIR /app
EXPOSE 80
COPY . .
ENTRYPOINT ["dotnet", "Opala.WebApi.dll"]