namespace Community.Core.Exceptions
{
   public class UserNotFoundException: Exception
    {
        public UserNotFoundException(string userName):base($"No user found with username: {userName}") { }
    }
}
