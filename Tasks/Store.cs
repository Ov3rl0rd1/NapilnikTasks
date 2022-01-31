using System;
using System.Collections.Generic;

namespace Napilnik.NapilnikTasks.Tasks
{

    public class Good
    {
        public readonly string Name;

        public Good(string name)
        {
            Name = name;
        }
    }

    public class Warehouse
    {
        public IReadOnlyDictionary<Good, int> Goods => _goods;

        private Dictionary<Good, int> _goods = new Dictionary<Good, int>();

        public void Delive(Good good, int count)
        {
            if (_goods.ContainsKey(good))
                _goods[good] += count;
            else
                _goods.Add(good, count);
        }

        public void TryRemoveGoods(Good good, int count)
        {
            if (_goods.ContainsKey(good) == false)
                throw new ArgumentException(nameof(good));

            if (_goods[good] < count)
                throw new ArgumentException(nameof(count));

            _goods[good] -= count;

            if (_goods[good] == 0)
                _goods.Remove(good);
        }
    }

    public class Shop
    {
        private Warehouse _warehouse;

        public Shop(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public Cart Cart()
        {
            return new Cart(_warehouse);
        }
    }

    public class Cart
    {
        private Dictionary<Good, int> _cart = new Dictionary<Good, int>();
        private Warehouse _warehouse;

        public Cart(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public void Add(Good good, int count)
        {
            _warehouse.TryRemoveGoods(good, count);

            if (_cart.ContainsKey(good))
                _cart[good] += count;
            else
                _cart.Add(good, count);
        }

        public OrderInfo Order()
        {
            var orderInfo = new OrderInfo(_cart);
            _cart.Clear();
            return orderInfo;
        }
    }

    public class OrderInfo
    {
        public readonly string Paylink;

        public OrderInfo(Dictionary<Good, int> cart)
        {
            Paylink = "byjygrh";
        }
    }

    class Store
    {
        public Store()
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

            //Вывод всех товаров на складе с их остатком

            Cart cart = shop.Cart();
            cart.Add(iPhone12, 4);
            cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

            //Вывод всех товаров в корзине

            Console.WriteLine(cart.Order().Paylink);

            cart.Add(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары
        }
    }
}
