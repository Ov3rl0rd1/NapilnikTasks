using System;

namespace IMJunior
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var orderForm = new OrderForm();
            var paymentHandler = new PaymentHandler();

            var systemId = orderForm.ShowForm();

            paymentHandler.ShowPaymentResult(systemId);
        }
    }
}
