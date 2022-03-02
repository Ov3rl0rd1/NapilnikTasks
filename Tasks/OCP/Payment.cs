namespace IMJunior
{

    public class Payment : IPayment
    {
        private readonly string _id;

        private readonly string _callAPI;

        public Payment(string id, string callAPI)
        {
            _id = id;
            _callAPI = callAPI;
        }

        public string Id => _id;

        public string CallAPI => _callAPI;
    }
}
