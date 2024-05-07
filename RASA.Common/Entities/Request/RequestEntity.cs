namespace RASA.Common.Entities.Request;

public class RequestEntity
{
    public string Url { get; set; }
    public RequestType Method { get; set; }
    public Dictionary<string,string>? Header { get; set; }
    
    public  IEnumerable<KeyValuePair<string,string>>? Body { get; set; }
}