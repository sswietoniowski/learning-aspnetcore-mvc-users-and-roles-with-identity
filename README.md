# Learning ASP.NET Core MVC - Users & Roles (Identity)

Small example showing how to manage basic authentication (cookie based) and authorization (with roles) in an ASP.NET Core MVC app - with Identity support.

This sample was created for my own use during lectures, so nothing fancy here :-).

Please remember that to start this app, you have to:

- create database by using `Update-Database` (you might need to change the connection string to do that),
- to login in as a cumstomer you have to use username "john.doe@getnada.com" and password "P@ssw0rd".

More information about Identity in ASP.NET Core MVC can be found here: 

- [ASP.NET Core MVC Login and Registration using Identity](https://youtu.be/CzRM-hOe35o),
- [ASP.NET Core Identity Tutorial](https://www.tektutorialshub.com/asp-net-core/asp-net-core-identity-tutorial/),
- [User Registration & login Using Cookie Authentication ASP.NET Core](https://www.tektutorialshub.com/asp-net-core/user-registration-login-using-cookie-authentication-asp-net-core/),
- [Scaffold Identity in ASP.NET Core projects](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-7.0&tabs=visual-studio).

Other useful resources (if you want to tweak your UI a little bit :-)):

- [Use LibMan with ASP.NET Core in Visual Studio](https://learn.microsoft.com/en-us/aspnet/core/client-side/libman/libman-vs?view=aspnetcore-7.0), 
- [How to Install Bootstrap Package in ASP.NET Core Application in Visual Studio](https://www.yogihosting.com/install-bootstrap-aspnet-core/).

If you want to test that you can register as a new user (with an e-mail verification), you might use [Nada](https://getnada.com/).
