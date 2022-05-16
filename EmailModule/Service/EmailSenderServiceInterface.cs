using EmailModule.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailModule.Service
{
    public interface EmailSenderServiceInterface
    {
        Task SendEmail(Message message);
    }
}
