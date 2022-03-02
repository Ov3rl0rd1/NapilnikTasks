using System;
using System.Collections.Generic;

namespace IMJunior
{
    public class PaymentHandler
    {
        public IReadOnlyCollection<IPayment> PaymentMethods;

        public PaymentHandler()
        {
            List<IPayment> paymentMethods = new List<IPayment>();
            paymentMethods.Add(CreatePaymentMethod("QIWI", "Перевод на страницу QIWI..."));
            paymentMethods.Add(CreatePaymentMethod("WebMoney", "Вызов API WebMoney..."));
            paymentMethods.Add(CreatePaymentMethod("Card", "Вызов API банка эмитера карты Card..."));
            PaymentMethods = paymentMethods;
        }

        public void ShowPaymentResult(string systemId)
        {
            IPayment paymentMethod = null;
            foreach (var e in PaymentMethods)
                if (e.Id == systemId)
                    paymentMethod = e;

            if (paymentMethod == null)
                throw new ArgumentException(nameof(systemId));

            Console.WriteLine(paymentMethod.CallAPI);
            Console.WriteLine($"Вы оплатили с помощью {systemId}");
            Console.WriteLine($"Проверка платежа через {systemId}...");
            Console.WriteLine("Оплата прошла успешно!");
        }

        protected virtual IPayment CreatePaymentMethod(string id, string callAPI)
        {
            return new Payment(id, callAPI);
        }
    }
}
