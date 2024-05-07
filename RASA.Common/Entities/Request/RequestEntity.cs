namespace RASA.Common.Entities.Request;

public class RequestEntity
{
    public string Url { get; set; }
    public RequestType Method { get; set; }
    public Dictionary<string,object> Header { get; set; }
    public Dictionary<string,object> Body { get; set; }
}