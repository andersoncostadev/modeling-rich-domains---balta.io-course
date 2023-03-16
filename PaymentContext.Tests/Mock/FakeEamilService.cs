using PaymentContext.Domain.Services;

namespace PaymentContext.Tests.Mock
{
    [TestClass]
    public class FakeEamilService : IEmailService
    {
        [TestMethod]
        public void Send(string to, string email, string subject, string body)
        {
           
        }
    }
}
