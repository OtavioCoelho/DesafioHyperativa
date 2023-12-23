namespace DesafioHyperativa.API.Infra;

public class CustomModelError
{
    public string Status { get; set; }
    public string Title { get; set; }
    public object Errors { get; set; }
}
