﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Core.Interfaces
{
   public interface ITokenClaimsService
    {
        Task<string> GetTokenAsync(string userName);
    }
}
