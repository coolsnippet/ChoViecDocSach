

<!-- TOC insertAnchor:true orderedList:true -->

### 1. nuget source:

https://www.nuget.org/
NuGet feed v3 (VS 2015 / NuGet v3.x): https://api.nuget.org/v3/index.json
NuGet feed v2 (VS 2013 and earlier / NuGet 2.x): https://www.nuget.org/api/v2


### 2. and setting

	go here: %appdata%\NuGet
	chage the proxy line:	
	<configuration>
		<!-- stuff -->
		<config>
			<add key="http_proxy" value="http://my.proxy.address:port" />
			<add key="http_proxy.user" value="mydomain\myUserName" />
			<add key="http_proxy.password" value="base64encodedHopefullyEncryptedPassword" />
		</config>
		<!-- stuff -->
	</configuration>
	
	reading: http://stackoverflow.com/questions/9232160/nuget-behind-proxy
             http://stackoverflow.com/questions/11553591/visual-studio-c-nuget-unable-to-connect-to-remote-server
            

### and then uncheck other nuget sources (feed)

http://stackoverflow.com/questions/37054469/nuget-package-feed-vsts-exception-system-aggregateexception-thrown-when-t