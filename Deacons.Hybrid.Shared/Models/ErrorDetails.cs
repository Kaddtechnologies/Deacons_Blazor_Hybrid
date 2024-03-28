using System.Text.Json;

namespace Deacons.Hybrid.Shared.Models;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
public class ErrorResponse
{
    public List<ErrorModel> Error { get; set; } = new List<ErrorModel>();
    public bool Successful { get; set; }
}
public class ErrorModel
{
    public string FieldName { get; set; }
    public string Message { get; set; }
}
