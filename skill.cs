namespace GameSkillProblem
{
    public class Character
    {
        public string Name { get; set; }
        
        public int CurrentHP { get; private set; }
        
        public int MaxHP { get; private set; }
        
        public int CurrentMana { get; private set; }
        
        public int MaxMana { get; private set; }

        public Character(string name, int maxHP = 100, int maxMana = 100)
        {
            Name = name;
            MaxHP = maxHP;
            CurrentHP = maxHP;
            MaxMana = maxMana;
            CurrentMana = maxMana;
        }

        public Character(string name, int currentHP, int maxHP, int currentMana, int maxMana)
        {
            Name = name;
            MaxHP = maxHP;
            CurrentHP = Math.Max(0, Math.Min(currentHP, maxHP));
            MaxMana = maxMana;
            CurrentMana = Math.Max(0, Math.Min(currentMana, maxMana));
        }

        public bool IsAlive() => CurrentHP > 0;

        public void TakeDamage(int damage)
        {
            if (damage > 0)
            {
                CurrentHP = Math.Max(0, CurrentHP - damage);
            }
        }

        public void Heal(int amount)
        {
            if (amount > 0)
            {
                if (amount >= MaxHP - CurrentHP)
                {
                    CurrentHP = MaxHP;
                }
                else
                {
                    CurrentHP += amount;
                }
            }
        }

        public bool HasEnoughMana(int cost) => CurrentMana >= cost;

        public void UseMana(int cost)
        {
            if (HasEnoughMana(cost))
            {
                CurrentMana -= cost;
            }
        }

        public void RestoreMana(int amount)
        {
            if (amount > 0)
            {
                if (amount >= MaxMana - CurrentMana)
                {
                    CurrentMana = MaxMana;
                }
                else
                {
                    CurrentMana += amount;
                }
            }
        }
    }

    public class Skill
    {
        private int _cooldown;
        private int _mana;
        private int _damage;
        private string? _target;
        private string? _animation;

        public int Cooldown
        {
            get => _cooldown;
            set
            {
                if (value < 0 || value > 60)
                {
                    throw new ArgumentOutOfRangeException(nameof(Cooldown), 
                        $"Cooldown must be between 0 and 60 seconds. Provided value: {value}");
                }
                _cooldown = value;
            }
        }

        public int Mana
        {
            get => _mana;
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(Mana), 
                        $"Mana must be between 0 and 100. Provided value: {value}");
                }
                _mana = value;
            }
        }

        public int Damage
        {
            get => _damage;
            set => _damage = value;
        }

        public string? Target
        {
            get => _target;
            set => _target = value;
        }

        public string? Animation
        {
            get => _animation;
            set => _animation = value;
        }

        public int CurrentCooldown { get; private set; }

        [Obsolete("Use Character class for mana management instead")]
        public int AvailableMana { get; private set; }

        public Skill(int cooldown, int mana, int damage, string? target = null, string? animation = null)
        {
            Cooldown = cooldown;
            Mana = mana;
            Damage = damage;
            Target = target;
            Animation = animation;
            CurrentCooldown = 0;
        }

        internal void SetCurrentCooldown(int value)
        {
            CurrentCooldown = value;
        }

        public bool Cast(Character caster, Character? target)
        {
            if (!caster.IsAlive())
            {
                return false;
            }

            if (CurrentCooldown > 0)
            {
                return false;
            }

            if (!caster.HasEnoughMana(Mana))
            {
                return false;
            }

            if (Target != null && target == null)
            {
                return false;
            }

            if (target != null && !target.IsAlive())
            {
                return false;
            }

            caster.UseMana(Mana);
            
            if (target != null && Damage > 0)
            {
                target.TakeDamage(Damage);
            }
            
            CurrentCooldown = Cooldown;
            return true;
        }

        public void ReduceCooldown()
        {
            if (CurrentCooldown > 0)
            {
                CurrentCooldown--;
            }
        }
    }
}
