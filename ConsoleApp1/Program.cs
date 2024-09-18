using System;
using System.Net.Http;
using System.Threading.Tasks;
using TechExpertRef;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string text = "гражданский кодекс";
            string url = "http://192.168.0.14:81/kodeks/bparser?parse=" + Uri.EscapeDataString(text);

            try
            {
                using (HttpClient httpclient = new HttpClient())
                {
                    HttpResponseMessage response = await httpclient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string bparserResult = await response.Content.ReadAsStringAsync();

                    var client = new apiSoapClient();
                    client.Endpoint.EndpointBehaviors.Add(new CustomBehavior());

                    var fuzzySearchRequest = new FuzzySearchRequest("гражданский кодекс", null, 0, "searchbynames", bparserResult);
                    var resFuzzySearch = await client.FuzzySearchAsync(fuzzySearchRequest);

                    var getDocsInfoRequest = new GetDocsInfoRequest("901714421", "1");
                    var resGetDocsInfo = await client.GetDocsInfoAsync(getDocsInfoRequest);
                    // сервер возвращает:
                    //<env:Envelope xmlns:env="http://schemas.xmlsoap.org/soap/envelope">
                    //   <env:Body>
                    //      <env:Fault>
                    //         <faultcode>env:Server</faultcode>
                    //         <faultstring>No license for attributes functions</faultstring>
                    //      </env:Fault>
                    //   </env:Body>
                    //</env:Envelope>
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка запроса: {ex.Message}");
            }
        }
    }
}

