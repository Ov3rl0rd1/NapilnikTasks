using System;

namespace Napilnik
{
    class Weapon
    {
        public int Damage { get; private set; }
        public int Bullets { get; private set; }

        public Weapon(int damage, int bullets)
        {
            Damage = damage;
            Bullets = bullets;
        }

        public void Fire(Player player)
        {
            player.TryTakeDamage(Damage);
            Bullets -= 1;
        }
    }

    class Player
    {
        private float _health;

        public void TryTakeDamage(float damage)
        {
            _health -= damage;
            
            if (_health <= 0)
            {
                _health = 0;
                Console.WriteLine("Player dead");
            }
        }
    }

    class Bot
    {
        public Weapon Weapon { get; private set; }

        public Bot(Weapon weapon)
        {
            Weapon = weapon;
        }
        
        public void OnSeePlayer(Player player)
        {
            Weapon.Fire(player);
        }
    }
}
