
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

    
and with moq with repository: 
https://www.safaribooksonline.com/library/view/aspnet-mvc-4/9781430247739/Sec70_9781430247739_Ch09.xhtml 

test json message with POST:

[Fact]
public async void PostCanRespondInJson()
{
    var message = new MessageDto
    {
        Text = "This is JSON"
    };
    var response = await _server.HttpClient.PostAsJsonAsync("/hello", message);
    var result = await response.Content.ReadAsAsync<MessageDto>(new[] { new JsonMediaTypeFormatter() });

    Assert.Equal(message.Text, result.Text);
}

testing with json
http://stackoverflow.com/questions/39020142/load-json-string-to-httprequestmessage

  var json = JsonConvert.SerializeObject(sr);
        //construct content to send
        var content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage {
            RequestUri = new Uri("http://localhost/api/shoppingcart"),
            Content = content
        };

        var controller = new ShoppingCartController();
        //Set a fake request. If your controller creates responses you will need this
        controller.Request = request;