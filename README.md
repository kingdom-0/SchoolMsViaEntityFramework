# SchoolMsViaEntityFramework
1------------Install EntityFramework
nuget:Install-Package EntityFramework
2------------Config entity framework databaseInitializer
<entityFramework>
    <contexts>
      <context type="SchoolMsViaEntityFramework.DAL.SchoolContext, SchoolMsViaEntityFramework">
        <databaseInitializer type="SchoolMsViaEntityFramework.DAL.SchoolInitializer, SchoolMsViaEntityFramework" />
      </context>
    </contexts>
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.LocalDbConnectionFactory, EntityFramework">
      <parameters>
        <parameter value="v11.0" />
      </parameters>
    </defaultConnectionFactory>
    <providers>
      <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
    </providers>
  </entityFramework>
  3.Config SQL connection string
  <connectionStrings>
    <add name="SchoolContext" connectionString="Data Source=(LocalDb)\v11.0;Initial Catalog=ContosoUniversity1;Integrated Security=SSPI;" providerName="System.Data.SqlClient"/>
</connectionStrings>
<appSettings>
  <add key="webpages:Version" value="3.0.0.0" />
  <add key="webpages:Enabled" value="false" />
  <add key="ClientValidationEnabled" value="true" />
  <add key="UnobtrusiveJavaScriptEnabled" value="true" />
</appSettings>teambition
4-------------If occured "调用的目标发生了异常", 请尝试添加EntityFramework.SqlServerCompact(nuget)
5-------------If visual studio is 2015 or higher, replace SqlServer instance from from v11.0 to MSSQLLocalDB
 