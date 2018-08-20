# IISHealthMonitor
Edit App.config and enter the configuration values for your environment.
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="EmailSettingsFrom" value="" />
    <add key="EmailSettingsTo" value="" />
    <add key="EmailSettingsPassword" value="" />
    <add key="EmailSettingsDisplayName" value="" />
    <add key="EmailSettingsSMTPServer" value="" />
    <add key="EmailSettingsSMTPServerPort" value="" />
    <add key="LogPath" value="C:\inetpub\logs\LogFiles\W3SVC1"/>
  </appSettings>
</configuration>



This is a netcore build, to execute after building 

To Execute

```console

dotnet HealthMonitor.dll 

```
