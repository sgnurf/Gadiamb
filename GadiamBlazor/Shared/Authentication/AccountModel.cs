namespace GadiamBlazor.Shared.Authentication
{
    public class AccountModel
    {
        public bool EmailConfirmed { get; set; }

        public string Email { get; set; } = default!;

        public string UserName { get; set; } = default!;

        public bool IsAdmin { get; set; }
    }
}