using PaymentContext.Domain.Entites;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Mock
{
    
    [TestClass]
    public class FakeStudentRepository : IStudentRepository
    {
        [TestMethod]
        public bool DocumentExists(string document)
        {
            if(document == "99999999999")
                return true;
            return false;
        }

        [TestMethod]
        public bool EmailExists(string email)
        {
            if (email == "email@email.io")
                return true;
            return false;
        }

        [TestMethod]
        public void CreateSubscription(Student student)
        {
          
        }    
    }
}
