using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.ValueObject;

namespace PaymentContext.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(new Contract<Notifiable<Notification>>()
                .Requires()
                .IsEmail(Address, "Email.Adress", "E-mail inválido")
            );
        }

        public string Address { get; private set; }
    }
}
