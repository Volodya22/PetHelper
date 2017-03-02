using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHelperApi.Services
{
    public interface IMail
    {
        void Send(string to, string subject, string body, int timeout = 10000);
    }
}
