namespace CrimeWatch.Application.Specifications;
public class ModeratorByPoliceId : Specification<Moderator>
{
    public ModeratorByPoliceId(string policeId) : base(e => e.PoliceId.Equals(policeId)) { }
}
