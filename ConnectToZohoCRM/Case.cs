using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json;

using System.Text.Json.Serialization;

using System.Text.Json.Serialization;
using Newtonsoft.Json;

public class Model
{
    public List<Case> items { get; set; }
}

public record class data()
{
    public Case caseCRM { get; set; }
}
public record class Deal_Name1()
{
    [JsonProperty("Deal_Name")]
    public string name { get; set; }
}
public record class Case()

    {

    [JsonProperty("data")]
    public string data { get; set; }

    [JsonProperty("id")]
    public string id { get; set; }

    [JsonProperty("Subject")]
    public string Subject { get; set; }

    [JsonProperty("Case_Number")]
    public string Case_Number { get; set; }

    [JsonProperty("Deal_Name")]

    public Deal_Name1 Deal_Name { get; set; } 
}

public class Token
{
    public string access_token { get; set; }
    public string scope { get; set; }
}

namespace WebAPIClient
{
    internal class Case
    {
    }

}
