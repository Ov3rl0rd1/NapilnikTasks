using System;

namespace Napilnik
{
    internal class Weapon
    {
        public readonly int Damage;

        private int _bullets;

        public Weapon(int damage, int bullets)
        {
            if (damage < 0)
                throw new ArgumentException(nameof(damage));
            if (bullets < 0)
                throw new ArgumentException(nameof(bullets));

            Damage = damage;
            _bullets = bullets;
        }

        public virtual bool CanShoot => _bullets > 0;

        public void Fire(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            if (CanShoot)
                throw new InvalidOperationException();

            player.TryTakeDamage(Damage);
            _bullets -= 1;
        }

    }

    internal class Player
    {
        private float _health;

        public Player(float health)
        {
            if (health <= 0)
                throw new ArgumentOutOfRangeException(nameof(health));

            _health = health;
        }

        public float Health => _health;

        public void TryTakeDamage(float damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

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
            if (weapon == null)
                throw new ArgumentNullException(nameof(weapon));

            _weapon = weapon;
        }

        public void OnSeePlayer(Player player)
        {
            if (_weapon.CanShoot)
                _weapon.Fire(player);
        }
    }
}
