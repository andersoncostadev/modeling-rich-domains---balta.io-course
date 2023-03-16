using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entites;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : 
        Notifiable<Notification>, 
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePayPalSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository= studentRepository;
            _emailService= emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validations
            command.Validate();
            if(command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel realizar sua assinatuira");
            }

            //Verificar se o Documento já está cadastrado
            if (_studentRepository.DocumentExists(command.Document))
                AddNotification("Document", "Este Documento já está cadastrado");

            //Verificar se o Email já está cadastrado
            if (_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está cadastrado");

            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street,
                command.Number,
                command.Neighborhood,
                command.City,
                command.State,
                command.Country,
                command.ZipCode
           );

            //Gerar Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email
           );

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar as notificações
            if (IsValid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            //Salvar as informações
            _studentRepository.CreateSubscription(student);

            //Enviar E-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Seja bem vindo ao balta.io", "Sua assinatura foi gerada");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
     
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {

            //Verificar se o Documento já está cadastrado
            if (_studentRepository.DocumentExists(command.Document))
                AddNotification("Document", "Este Documento já está cadastrado");

            //Verificar se o Email já está cadastrado
            if (_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está cadastrado");

            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street,
                command.Number,
                command.Neighborhood,
                command.City,
                command.State,
                command.Country,
                command.ZipCode
           );

            //Gerar Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode,//só muda a implementação do pagamento
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email
           );

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar as informações
            _studentRepository.CreateSubscription(student);

            //Enviar E-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Seja bem vindo ao balta.io", "Sua assinatura foi gerada");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}
