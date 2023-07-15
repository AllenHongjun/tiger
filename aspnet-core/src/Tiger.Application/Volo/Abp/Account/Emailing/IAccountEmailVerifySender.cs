using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Volo.Abp.Account.Emailing
{
    public interface IAccountEmailVerifySender
    {
        Task SendMailLoginVerifyCodeAsync(
        string code,
        string userName,
        string emailAddress);
    }
}
