using src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Services
{
    public interface IADLogin
    {
        bool verifyADUser(string username, string password, string key);
    }
}
