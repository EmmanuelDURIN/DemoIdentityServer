# Demonstration of IdentityServer 4 in ASP.Net Core Servers

Featuring many servers for separation of concerns
	
## ApiIdentityServer 
	
 - IdentityServer app with persistence using AspNetIdentity/EntityFramework

## CoursesWebApi 
	
 - Wep Api serving Rest resources
 - Protection using OpenId scopes and a Policy

## ImplicitFlowMvcClient

 - ASP.Net Core MVC client using ApiIdentityServer for the identity of the end-users
 - Implicit flow grant client is used
 - Identity of the client flows to ASP.Net Core application

## SPAImplicitClient

 - Basic spa client calling Rest apis from CoursesWebApi.
 - Implicit flow grant client is used with oidc-client.js (Logout is provided)
 




