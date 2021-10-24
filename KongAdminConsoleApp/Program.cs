/*
   https://medium.com/devopsturkiye/kong-api-gateway-installing-configuring-and-securing-dfea423ee53c

1. From local Kong machine API should be built:
        curl -X POST http://localhost:8001/services/admin-api/routes --data paths\=/admin-api

   Only than client may call it via port 8000: 
        var url = "http://192.168.14.118:8000/admin-api";
        var method = Method.GET;
*/

using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using RestSharp;

namespace KongAdminConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //       curl - i - X POST http://localhost:8001/services/test-service/routes \
            //-H "Content-Type: application/json" \
            //-d '{"name": "test-route", "paths": [ "/path/one", "/path/two" ]}'

            //{
            //    //var url = "http://192.168.14.118:8002/default/services";
            //    var url = "http://192.168.14.118:8000/admin-api";
            //    var method = Method.GET;

            //    RestClient client = new(url);
            //    client.Timeout = -1;
            //    var request = new RestRequest(method);
            //    //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            //    var response = client.Execute(request);
            //    Console.WriteLine(response.Content);
            //}

            const string ip = "192.168.14.118";

            //{
            //    RestClientEx client = new($"http://{ip}:8002/default/certificates");
            //    RestRequest request = new(Method.GET);
            //    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            //    var response = client.Execute(request);
            //    Console.WriteLine(response.Content);
            //}

            {
                RestClientEx client = new($"https://{ip}:8443/k",
                    (object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors) => 
                    {
                        Console.WriteLine($"\nCertificate:\n{certificate}");
                        Console.WriteLine($"\nChain:\n{chain}\n\n");
                        return true; 
                    });
                client.Timeout = -1;
                RestRequest request = new(Method.GET);
                request.AddHeader("auth", "kanpur");
                //request.AddHeader("Cookie", "d8fdb0c7bbb290989fe5b71584d38355=bn2pn83p1a6kklp8cd34fv6n74");
                var response = client.Execute(request);
                Console.WriteLine(response.Content);
            }

            {
                RestClientEx client = new($"https://{ip}:8443/mb");
                client.Timeout = -1;
                RestRequest request = new(Method.GET);
                request.AddHeader("auth", "mb");
                //request.AddHeader("Cookie", "d8fdb0c7bbb290989fe5b71584d38355=c4055abjjngtip967h56bqg7q3");
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
            }

            Console.WriteLine("\n\nPress any key to quit...");
            Console.ReadKey();
        }
    }
}
