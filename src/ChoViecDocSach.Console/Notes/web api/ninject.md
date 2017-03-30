
install the following 2 packages:

install-package Ninject.Web.WebApi
install-package Ninject.Web.WebApi.WebHost

config your things here:
The file NinjectWebCommon will be added to the App_Start folder. You need to the RegisterServices method and add your binding, like this:
kernel.Bind<IUserRepository>().To<UserRepository>();


reading:
http://ralbu.com/using-ninject-with-asp-net-web-api-2