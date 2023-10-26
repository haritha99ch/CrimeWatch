namespace CrimeWatch.Application.Specifications;
public class AccountByEmail : Specification<Account>
{
    public AccountByEmail(string email) : base(e => e.Email == email) { }
}
