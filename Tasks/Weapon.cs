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
            if (damage > _health)
                throw new ArgumentException(nameof(damage));

            _health -= damage;
        }
    }

    class Bot
    {
        public Weapon Weapon;

        public void OnSeePlayer(Player player)
        {
            Weapon.Fire(player);
        }
    }
}