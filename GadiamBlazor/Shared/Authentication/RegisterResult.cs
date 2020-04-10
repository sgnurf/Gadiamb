using System.Collections.Generic;

namespace GadiamBlazor.Shared.Authentication
{
    public class RegisterResult
    {
        public bool Successful { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}