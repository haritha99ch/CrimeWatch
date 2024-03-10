namespace Domain.Common.Models;
public abstract record EntityId(Guid Value)
{
    public override string ToString() => Value.ToString();
}
