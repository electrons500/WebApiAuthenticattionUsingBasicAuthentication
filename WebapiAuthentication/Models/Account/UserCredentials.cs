namespace WebapiAuthentication.Models.Account
{
    public class UserCredentials
    {
        public bool ValidateUserCredentials(string username, string password)
        {
            //check if username and password are correct.Return true if they are correct
            return username.Equals("user") && password.Equals("password123");
        }
    }
}
