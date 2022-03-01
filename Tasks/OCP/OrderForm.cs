using System;
using System.Linq;

namespace IMJunior
{
    public class OrderForm
    {
        public string ShowForm(PaymentHandler paymentHandler)
        {
            Console.WriteLine($"Мы принимаем: {string.Join(", ", paymentHandler.PaymentMethods.Select(x => x.Id))}");

            Console.WriteLine("Какое системой вы хотите совершить оплату?");
            return Console.ReadLine();
        }
    }
}
