using PaymentContext.Domain.Entites;

namespace PaymentContext.Domain.Repositories
{
    public interface IStudentRepository
    {
        bool DocumentExists(string document);
        bool EmailExists(string email);
        void CreateSubscription(Student student);
    }
}
