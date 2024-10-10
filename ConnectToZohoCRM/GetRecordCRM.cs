
public class Rootobject
{
    public Datum[] data { get; set; }
    public Info info { get; set; }
}

public class Info
{
    public int per_page { get; set; }
    public string next_page_token { get; set; }
    public int count { get; set; }
    public string sort_by { get; set; }
    public int page { get; set; }
    public object previous_page_token { get; set; }
    public DateTime page_token_expiry { get; set; }
    public string sort_order { get; set; }
    public bool more_records { get; set; }
}

public class Datum
{
    public Deal_Name Deal_Name { get; set; }
    public string Case_Number { get; set; }
    public Account_Name Account_Name { get; set; }
    public string id { get; set; }
    public string Subject { get; set; }
}

public class Deal_Name
{
    public string name { get; set; }
    public string id { get; set; }
}

public class Account_Name
{
    public string name { get; set; }
    public string id { get; set; }
}
