using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        using (var httpClient = new HttpClient())
        {
            // Substitua a URL pela URL real da sua API
            var apiUrl = "https://localhost:7164//usuario";

            // Exemplo de solicitação GET para listar usuários
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine($"Falha na solicitação: {response.StatusCode}");
            }
        }
    }
}
