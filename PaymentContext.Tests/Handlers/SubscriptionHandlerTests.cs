using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]

        public void ShouldReturnErrorWhenDocumentExists() 
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEamilService());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName= "Bruce";
            command.LastName = "Wayne";
            command.Document = "99999999999";
            command.Email = "email@email.io2";
            command.BarCode = "123456789";
            command.BoletoNumber = "12345789";
            command.PaymentNumber = "123654";
            command.PaidDate = DateTime.Now;
            command.ExpireDate= DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "WAYNE COPR";
            command.PayerDocument = "12345678911";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "batman@dc.com";
            command.Street = "BatCaverna";
            command.Number = "1";
            command.Neighborhood = "Esconderijo do Batman";
            command.City= "Gothan";
            command.State= "New Jersey";
            command.Country= "Estados Unidos";
            command.ZipCode= "1234567";

            handler.Handle(command);
            Assert.AreEqual(true, handler.IsValid);

        }

    }
}
