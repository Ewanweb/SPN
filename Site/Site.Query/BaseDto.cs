namespace Site.Query;

public class BaseDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? UpdatedDate { get; protected set; }
}