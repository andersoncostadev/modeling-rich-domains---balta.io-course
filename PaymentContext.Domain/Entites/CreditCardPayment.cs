using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entites
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(
            string cardHolderName,
            string cardnumber,
            string lastTransactionNumber,
            DateTime paidDate,
            DateTime expireDate,
            decimal total,
            decimal totalPaid,
            string payer,
            Document document,
            Address address,
            Email email) : base(paidDate, expireDate, total, totalPaid, payer, document, address, email)
        {
            CardHolderName = cardHolderName;
            Cardnumber = cardnumber;
            LastTransactionNumber = lastTransactionNumber;
        }

        public string CardHolderName { get; private set; }

        public string Cardnumber { get; private set; }

        public string LastTransactionNumber { get; private set; }
    }
}
