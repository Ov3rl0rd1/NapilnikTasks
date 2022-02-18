using System;

namespace Napilnik
{
    internal class Weapon
    {
        private int _damage;

        private int _bullets;

        public Weapon(int damage, int bullets)
        {
            _damage = damage;
            _bullets = bullets;
        }

        public int Damage => _damage;

        public int Bullets => _bullets;

        public void Fire(Player player)
        {
            player.TryTakeDamage(_damage);
            _bullets -= 1;
        }
    }

    internal class Player
    {
        private float _health;

        public Player(float health)
        {
            _health = health;
        }

        public float Health => _health;

        public void TryTakeDamage(float damage)
        {
            if (damage > _health)
                damage = _health;

            _health -= damage;
        }
    }

    internal class Bot
    {
        private Weapon _weapon;

        public Bot(Weapon weapon)
        {
            _weapon = weapon;
        }

        public Weapon Weapon => _weapon;

        public void OnSeePlayer(Player player)
        {
            _weapon.Fire(player);
        }
    }
}
