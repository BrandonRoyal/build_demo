# escape=`

# build agent
FROM sixeyed/msbuild:4.5.2-webdeploy
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

COPY . C:\src
RUN $nuGetPath = 'C:\Chocolatey\bin\nuget.bat'; `
    $msBuildPath = 'C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe'; `
    & $nuGetPath restore 'C:\src\Whale.sln'; `
    & $msBuildPath 'C:\src\Whale.Web\Whale.Web.csproj' /p:OutputPath=c:\out /p:DeployOnBuild=true /p:Configuration=Release /p:VSToolsPath=C:\MSBuild.Microsoft.VisualStudio.Web.targets.14.0.0.3\tools\VSToolsPath

FROM microsoft/aspnet:windowsservercore-10.0.14393.1066
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# disable DNS cache so container addresses always fetched from Docker
RUN Set-ItemProperty -path 'HKLM:\SYSTEM\CurrentControlSet\Services\Dnscache\Parameters' -Name ServerPriorityTimeLimit -Value 0 -Type DWord

RUN Remove-Website 'Default Web Site';

RUN Enable-WindowsOptionalFeature -Online -FeatureName IIS-ApplicationDevelopment,IIS-ASPNET45,IIS-BasicAuthentication,IIS-CommonHttpFeatures,IIS-DefaultDocument,IIS-DirectoryBrowsing,IIS-HealthAndDiagnostics,IIS-HttpCompressionStatic,IIS-HttpErrors,IIS-HttpLogging,IIS-ISAPIExtensions,IIS-ISAPIFilter,IIS-NetFxExtensibility45,IIS-Performance,IIS-RequestFiltering,IIS-Security,IIS-StaticContent,IIS-WebServer,IIS-WebServerRole,NetFx4Extended-ASPNET45

# Set up website: whale
RUN New-Item -Path 'C:\websites\Whale' -Type Directory -Force; 

RUN New-Website -Name 'whale' -PhysicalPath 'C:\websites\Whale' -Port 80 -Force; 

EXPOSE 80

COPY --from=0 C:\out\_PublishedWebsites\Whale.Web /websites/Whale
