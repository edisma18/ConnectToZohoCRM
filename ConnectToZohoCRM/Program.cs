using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

var repositories = await GetCasesAsync(client);

foreach (var repo in repositories)
{
    Console.WriteLine($"id: {repo.id}");
    Console.WriteLine($"Subject: {repo.Subject}");
    Console.WriteLine($"Case_Number : {repo.Case_Number}");
    if (repo.Deal_Name != null )
    {
        Console.WriteLine($"Deal_Name : {repo.Deal_Name.name}");

    }

    if (repo.Account_Name != null)
    {
        Console.WriteLine($"Account_Name : {repo.Account_Name.name}");

    }

    Console.WriteLine();
}

static async Task<IList<Datum>> GetCasesAsync(HttpClient client)
{

    var clientToken = new HttpClient();
    
    var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.zoho.com/oauth/v2/token?refresh_token=1000.9ac0b9f90522dcd7228a9c662d93651c.bee5f3ae49c4eef0b29aa4f2b72b8ebc&client_id=1000.WDK9A9IX2M3E7VQV7G60TX00R9JSWB&client_secret=f1aa7083bc81106754ca6379020596b83e4f956279&grant_type=refresh_token");

    //var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.zoho.com/oauth/v2/token?refresh_token=1000.977114ec827f39efec9977206a8de550.0f4ad2e0e0b642a94d36eb1bb472de5a&client_id=1000.WDK9A9IX2M3E7VQV7G60TX00R9JSWB&client_secret=f1aa7083bc81106754ca6379020596b83e4f956279&grant_type=refresh_token");
    request.Headers.Add("Cookie", "JSESSIONID=9C330F962792359E620D82159FAC5B2E; _zcsr_tmp=eceda221-658e-471b-b4a0-3eea21570e90; iamcsr=eceda221-658e-471b-b4a0-3eea21570e90; zalb_b266a5bf57=a7f15cb1555106de5ac96d088b72e7c8");
    var responseToken = await clientToken.SendAsync(request);
    responseToken.EnsureSuccessStatusCode();
    string resultTokenString = await responseToken.Content.ReadAsStringAsync();

    Console.WriteLine(resultTokenString);

    Token? accessToken =
               System.Text.Json.JsonSerializer.Deserialize<Token>(resultTokenString);

    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        "Bearer", accessToken.access_token);

    client.DefaultRequestHeaders.Accept.Add(
                                 new MediaTypeWithQualityHeaderValue("application/json"));

    HttpResponseMessage response = client.GetAsync("https://www.zohoapis.com/crm/v7/Cases?fields=Account_Name,Deal_Name,Subject,Case_Number&per_page=5").Result;  
    response.EnsureSuccessStatusCode();
    string resultContentString = await response.Content.ReadAsStringAsync();

    var jsonObject = JsonConvert.DeserializeObject(resultContentString);

    using var contentStream = await response.Content.ReadAsStreamAsync();
    var root = await System.Text.Json.JsonSerializer.DeserializeAsync<Rootobject>(contentStream);

    IList<Datum> casesFound = new List<Datum>();

    if (root != null)
    {
        foreach (var item in root.data)
        {
            casesFound.Add(item);
        }
    }

    /* JObject casesSearch = JObject.Parse(resultContentString);
    IList<JToken> results = casesSearch["data"].Children().ToList();
    IList<Case> searchResults = new List<Case>();

    foreach (JToken result in results)
    {
        Case searchResult = result.ToObject<Case>();
        searchResults.Add(searchResult);
    }
    */

    return casesFound;

}