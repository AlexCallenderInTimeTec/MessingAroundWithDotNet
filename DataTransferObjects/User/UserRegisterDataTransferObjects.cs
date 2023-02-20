using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessingAroundWithDotNet.DataTransferObjects.User
{
    public class UserRegisterDataTransferObjects
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}