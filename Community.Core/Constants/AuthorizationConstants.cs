using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Core.Constants
{
   public class AuthorizationConstants
    {
        public const string AUTH_KEY = "Community123456789~!@#$%^&*()_+";

        // TODO: Don't use this in production
        public const string DEFAULT_PASSWORD = "Community@Reddy";

        // TODO: Change this to an environment variable
        public const string JWT_SECRET_KEY = "Secreat@Community123456789~!@#$%^&*()_+";
    }
}
