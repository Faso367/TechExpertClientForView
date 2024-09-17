using System;
using System.Net.Http;
using System.Threading.Tasks;
using TechExpertRef;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{


    class Program
    {

        static async Task Main(string[] args)
        {
            string text = "гражданский кодекс";
            string url2 = "http://192.168.0.14:81/kodeks/bparser?parse=" + Uri.EscapeDataString(text);
            string url = "http://192.168.0.14:81/docs/api";


            using (HttpClient httpclient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpclient.GetAsync(url2);
                    response.EnsureSuccessStatusCode();
                    string bparserResult = await response.Content.ReadAsStringAsync();

                    var client2 = new apiSoapClient();
                    client2.Endpoint.EndpointBehaviors.Add(new CustomBehavior());

                    var resz = client2.FuzzySearch("гражданский кодекс", null, 0, "searchbynames", bparserResult);

                    var req3 = new GetDocsInfoRequest("901714421", "1");
                    Console.WriteLine();
                    var resp3 = client2.GetDocsInfo(req3);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Ошибка запроса: {e.Message}");
                }
            }
        }
    }

}

