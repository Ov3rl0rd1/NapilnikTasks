﻿using System;
using System.Collections.Generic;

namespace IMJunior
{
    public class PaymentHandler
    {
        private IReadOnlyCollection<IPayment> _paymentMethods = new IPayment[] { new QIWI(), new WebMoney(), new Card() };

        public void ShowPaymentResult(string systemId)
        {
            IPayment paymentMethod = null;
            foreach (var e in _paymentMethods)
                if (e.Id == systemId)
                    paymentMethod = e;

            if (paymentMethod == null)
                throw new ArgumentException(nameof(systemId));

            Console.WriteLine(paymentMethod.CallAPI);
            Console.WriteLine($"Вы оплатили с помощью {systemId}");
            Console.WriteLine($"Проверка платежа через {systemId}...");
            Console.WriteLine("Оплата прошла успешно!");
        }
    }
}
