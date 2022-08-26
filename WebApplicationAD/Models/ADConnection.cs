using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace WebApplicationAD.Models
{
    public class ADConnection
    {
        public static DirectoryEntry login1(string kullaniciadi,string password)
        {
            DirectoryEntry myDomain = new DirectoryEntry("LDAP://tarikbugra.com", kullaniciadi, password);//domain adı,kullanıcı adı şifre
            return myDomain;

        }
        public static DomainBilgileri login2(string kullaniciadi, string password)
        {

            PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, "tarikbugra.com", kullaniciadi, password);
            UserPrincipal userPrincipal = null;
            DomainBilgileri domainbilgi = new DomainBilgileri();
            try
            {
                userPrincipal = UserPrincipal.FindByIdentity(principalContext, IdentityType.SamAccountName, kullaniciadi);
                domainbilgi.isAccessSuccess = true;
            }
            catch
            {
                domainbilgi.isAccessSuccess = false;
                return domainbilgi;
            }
            if (userPrincipal == null)
            {
                domainbilgi.isAccountFound = false;
                return domainbilgi;
            }
            if (userPrincipal.SamAccountName == "")
            {
                domainbilgi.isAccountFound = false;
                return domainbilgi;
            }
            domainbilgi.isAccountFound = true;
            domainbilgi.AdiSoyadi = userPrincipal.DisplayName;
            domainbilgi.Description = userPrincipal.Description;
            domainbilgi.PhoneNumber = userPrincipal.VoiceTelephoneNumber;
            domainbilgi.Email = userPrincipal.EmailAddress;
            domainbilgi.SonOturumTarihi = userPrincipal.LastLogon.ToString();
            domainbilgi.SifreDegistirmeTarihi = userPrincipal.LastPasswordSet.ToString();
            if (userPrincipal.Enabled != null)
            {
                if (userPrincipal.Enabled == true)
                    domainbilgi.IsActive = true;
                else
                    domainbilgi.IsActive = false;
            }
            else
                domainbilgi.IsActive = false;
            if (userPrincipal.IsAccountLockedOut())
                domainbilgi.IsLocked = true;
            else
                domainbilgi.IsLocked = false;
            userPrincipal = null;
            principalContext = null;

            return domainbilgi;
        }
    }
}
