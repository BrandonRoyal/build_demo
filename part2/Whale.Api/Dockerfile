FROM microsoft/dotnet:sdk-nanoserver
COPY . /src
WORKDIR /src
RUN dotnet restore; dotnet publish -c Release -o /out

FROM microsoft/dotnet:nanoserver
WORKDIR /app
EXPOSE 5000
COPY --from=0 /out /app
ENTRYPOINT ["dotnet", "Whale.Api.dll"]