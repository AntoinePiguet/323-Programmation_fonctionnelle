using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    private static readonly HttpClient client = new HttpClient();
    private const string apiKey = "d357hhpr01qhorbgvj9gd357hhpr01qhorbgvja0";

    static async Task Main(string[] args)
    {
        string symbol = "AAPL";
        string date = "2025-09-11"; // format YYYY-MM-DD

        string url = $"https://finnhub.io/api/v1/stock/tick?symbol={symbol}&date={date}&token={apiKey}";

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // lève une exception si code != 200

            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
            Console.ReadLine();

        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Erreur : {e.Message}");
            Console.ReadLine();

        }
        Console.ReadLine();

    }
}


