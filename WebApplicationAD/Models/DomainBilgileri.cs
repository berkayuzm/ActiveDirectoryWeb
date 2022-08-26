using System.Collections.Generic;

namespace WebApplicationAD.Models
{
    public class DomainBilgileri
    {
        public bool isAccessSuccess { get; set; }
        public bool isAccountFound { get; set; }
        public string AdiSoyadi { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string SonOturumTarihi { get; set; }
        public string SifreDegistirmeTarihi { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        
    }
}
