using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Utility.Service.IService
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }

}
