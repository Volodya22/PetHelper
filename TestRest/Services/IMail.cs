namespace TestRest.Services
{
    public interface IMail
    {
        void Send(string to, string subject, string body, int timeout = 10000);
    }
}
