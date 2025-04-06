using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Company.Momen1.PL.Helper
{
    public interface IMailService
    {
        public void SendEmail(Email email);
    }
}
