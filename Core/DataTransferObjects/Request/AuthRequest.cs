namespace MyIssue.Core.DataTransferObjects.Request
{
    public class AuthRequest
    {
        public AuthRequest(){}
        public AuthRequest(string[] input)
        {
            Username = input[0];
            Password = input[1];
        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
