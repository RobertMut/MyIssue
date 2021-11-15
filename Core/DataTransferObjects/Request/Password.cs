namespace MyIssue.Core.DataTransferObjects.Request
{
    public class Password
    {
        public string UserLogin { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
