using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleWebScraper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();

            var responce = await httpClient.GetAsync("https://www.cvonline.lt/darbo-skelbimai/informacines-technologijos");

            var responceBody = await responce.Content.ReadAsStringAsync();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responceBody);

            var links = htmlDoc.DocumentNode.Descendants("a")
                .Where(node => node.GetAttributeValue("itemprop","").Contains("title")).ToList();
            var title = links.Select(l => l.InnerText);
            foreach (var item in title)
            {
                Console.WriteLine(item);
            }
            
        }
    }
}
