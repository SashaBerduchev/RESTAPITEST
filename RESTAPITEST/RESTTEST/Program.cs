using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RESTTEST
{
    class Program
    {
        private const string connstr = "https://tester.consimple.pro/";
        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter key!");
            ConsoleKeyInfo key = Console.ReadKey();
            if(key.Key.ToString() == "Enter")
            {
                Post.Send(connstr);
            }
            Console.ReadLine();
        }
    }

    public class Post
    {
        public static async void Send(string connstr)
        {
            try
            {
                HttpClient client = new HttpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpResponseMessage response = await client.GetAsync(connstr);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("WAIGHT!!!!");
                ParsData(responseBody);
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Trace.WriteLine(e.ToString());
            }
        }

        private static void ParsData(string responseBody)
        {
            Console.WriteLine();
            var root = JsonConvert.DeserializeObject<Root>(responseBody);
            for (int i = 0; i < root.Products.Count; i++)
            {
                for (int j = 0; j < root.Categories.Count; j++)
                {
                    if (root.Products[i].CategoryId == root.Categories[j].Id)
                    {
                        Console.WriteLine(i.ToString() + "." + " " + root.Products[i].Name + " | " + root.Categories[j].Name);
                    }
                }
            }
        }
    }

    public class Root
    {
        public List<Products> Products;
        public List<Categories> Categories;
    }
}
