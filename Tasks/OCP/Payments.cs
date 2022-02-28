namespace IMJunior
{
    class QIWI : IPayment
    {
        public string Id => "QIWI";

        public string CallAPI => "Перевод на страницу QIWI...";
    }

    class WebMoney : IPayment
    {
        public string Id => "WebMoney";

        public string CallAPI => "Вызов API WebMoney...";
    }

    class Card : IPayment
    {
        public string Id => "Card";

        public string CallAPI => "Вызов API банка эмитера карты Card...";
    }
}
