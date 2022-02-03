using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        var randomizer = new Random();

        var order = new Order(randomizer.Next(0, 100), randomizer.Next(0, 100000));

        IEnumerable<IPaymentSystem> paymentSystems = new IPaymentSystem[3]
            { new FirstPaymentSystem(), new SecondPaymentSystem(), new ThirdPaymentSystem(randomizer.Next(0, 100)) };


        foreach (var paymentSystem in paymentSystems)
            Console.WriteLine(paymentSystem.GetPaymentLink(order));
    }
}

public class Order
{
    public readonly int Id;
    public readonly int Amount;

    public Order(int id, int amount) => (Id, Amount) = (id, amount);
}

public interface IPaymentSystem
{
    string GetPaymentLink(Order order);
}

public class FirstPaymentSystem : IPaymentSystem
{
    private static string _prefix = "pay.system1.ru/order?";

    public string GetPaymentLink(Order order)
    {
        return $"{_prefix}amount={order.Amount}RUB&hash={Convertors.CreateMD5(order.Id)}";
    }
}

public class SecondPaymentSystem : IPaymentSystem
{
    private static string _prefix = "order.system2.ru/pay?";

    public string GetPaymentLink(Order order)
    {
        return $"{_prefix}hash={Convertors.CreateMD5(order.Id + order.Amount)}";
    }
}

public class ThirdPaymentSystem : IPaymentSystem
{
    private static string _prefix = "system3.com/pay?";
    private int _secretKey;

    public ThirdPaymentSystem(int secretKey)
    {
        _secretKey = secretKey;
    }

    public string GetPaymentLink(Order order)
    {
        return $"{_prefix}amount={order.Amount}&curency=RUB&hash={Convertors.CreateSHA1(order.Id + order.Id + _secretKey)}";
    }
}

public static class Convertors
{
    public static string CreateMD5(int input)
    {
        string hash;
        using (var md5 = MD5.Create())
        {
            hash = string.Concat(md5.ComputeHash(BitConverter
              .GetBytes(input))
              .Select(x => x.ToString("x2")));
        }
        return hash;
    }

    public static string CreateSHA1(int input)
    {
        var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input.ToString()));
        return string.Concat(hash.Select(b => b.ToString("x2")));
    }
}
