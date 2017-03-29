Consuming a web api:
example:

    public class WorkingClient
    {
        private HttpClient httpClient = new HttpClient();
        private Uri workingUrl;

        public WorkingClient()
        {
            var url = ConfigurationManager.AppSettings[Constants.PAYMENTFACADE_URL];

            if (url == null)
            {
                throw new ApplicationException("Missing workingUrl URL in app.config");
            }
            workingUrl = new Uri(url);
        }
        public WorkingClient(string workingUrl)
        {
            workingUrl = new Uri(workingUrl);            
        }

        public WorkingResponse WorkingOn(WorkingRequest workingRequest)
        {
            var content = new StringContent(JsonConvert.SerializeObject(purchaseRequest), Encoding.UTF8, "application/json");
            var responseTask = httpClient.PostAsync(workingUrl, content);
            var response = responseTask.Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<WorkingResponse>().Result;
            }
            throw new NotImplementedException();
        }


    }

add this to the nuget:
https://www.nuget.org/packages/System.Net.Http.Formatting.Extension/

furthur readings:
http://stackoverflow.com/questions/39190018/how-to-get-object-using-httpclient-with-response-ok-in-web-api

http://stackoverflow.com/questions/19448690/how-to-consume-a-webapi-from-asp-net-web-api-to-store-result-in-database

with authentication:
    httpClient.DefaultRequestHeaders.Add("Authorization", csstoken.UseToken);