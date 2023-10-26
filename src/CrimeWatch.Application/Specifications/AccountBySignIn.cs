namespace CrimeWatch.Application.Specifications;
internal class AccountBySignIn : Specification<Account>
{
    public AccountBySignIn(string email, string password)
        : base(e => e.Email.Equals(email) && e.Password.Equals(password)) { }
}
