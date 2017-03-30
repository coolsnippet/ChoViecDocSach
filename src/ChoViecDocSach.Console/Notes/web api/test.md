
This book is considered good: ASP.NET Web API 2 Recipes: A Problem Solution Approach

https://www.safaribooksonline.com/library/view/aspnet-web-api/9781430259817/9781430259800_Ch11.xhtml#Sec1

just test the controllers.

And external dependencies:
    - web service
    - database

to do the unit test for it, we need to refactor the implementation by using the dependency injection pattern

to implement the dependency injection,
    1. we create an interface
    2. create a concreate class with the external dependency
    3. modify the class to be tested

ASP.NET Web API has 2 primary mechanisms for dealing with controller dependency:
    IHttpControllerActivator
    IDependencyResolver

    

    