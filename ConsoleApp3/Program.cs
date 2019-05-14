using System;
using System.Net.Http;
using System.Xml;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your search term: \n");
            var query = Console.ReadLine();
            Request(query);
            Console.ReadKey();
        }
           
        public static async void Request(string query)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"http://detroit.craigslist.org/search/sss?format=rss&query={query}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(responseBody);
                    var items = xmlDoc.GetElementsByTagName("rdf:li");
                    if(items.Count != 0)
                    {
                        foreach (XmlElement kvp in items)
                        {
                            Console.WriteLine(kvp.GetAttribute("rdf:resource"));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No results found for that term!");
                    }    
                }
                catch(Exception e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine($"Message :{e.Message}");
                }
            }
        }

    }
}
