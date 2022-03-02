using System;

namespace IMJunior
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var orderForm = new OrderForm();
            var paymentHandler = new PaymentHandler();

            var systemId = orderForm.ShowForm(paymentHandler);

            paymentHandler.Pay(systemId);
        }
    }
}
