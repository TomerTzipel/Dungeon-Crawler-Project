

namespace PlayerSystem
{
    public class PlayerCombatEntity : CombatEntity
    {
        private Inventory _inventory;
        public bool DoesHUDNeedReprint { get; set; } = false;
        public PlayerCombatEntity(int maxHp, int damage, int evasion, int accuracy, int multihit, int armor, int pierce) 
            : base(maxHp, damage, evasion, accuracy, multihit, armor, pierce)
        {
            _inventory = new Inventory(this);
        }

        public Inventory Inventory
        {
            get
            {
                return _inventory;
            }
        }

        public override int Armor
        {
            get
            {
                int additiveBuff = Inventory.GetBuff(BuffType.Additive, StatType.Armor);
                float multiplicativeBuff = Inventory.GetBuff(BuffType.Multiplicative, StatType.Armor) / 100f;
                return (int)((base.Armor + additiveBuff) * multiplicativeBuff);
            }
        }

        public override int Damage
        {
            get
            {
                int additiveBuff = Inventory.GetBuff(BuffType.Additive, StatType.Damage);
                float multiplicativeBuff = Inventory.GetBuff(BuffType.Multiplicative, StatType.Damage) / 100f;
                return (int)((base.Damage + additiveBuff) * multiplicativeBuff);
            }
        }


        public override int Accuracy
        {
            get
            {
                int additiveBuff = Inventory.GetBuff(BuffType.Additive, StatType.Accuracy);
                float multiplicativeBuff = Inventory.GetBuff(BuffType.Multiplicative, StatType.Accuracy) / 100f;
                return (int)((base.Accuracy + additiveBuff) * multiplicativeBuff);
            }
        }

        public override int Evasion
        {
            get
            {
                if (Settings.Invincibility) return 100;

                int additiveBuff = Inventory.GetBuff(BuffType.Additive, StatType.Evasion);
                float multiplicativeBuff = Inventory.GetBuff(BuffType.Multiplicative, StatType.Evasion) / 100f;
                return (int)((base.Evasion + additiveBuff) * multiplicativeBuff);
            }
        }

        public override int Multihit
        {
            get
            {
                int additiveBuff = Inventory.GetBuff(BuffType.Additive, StatType.Multihit);
                float multiplicativeBuff = Inventory.GetBuff(BuffType.Multiplicative, StatType.Multihit) / 100f;
                return (int)((base.Multihit + additiveBuff) * multiplicativeBuff);
            }
        }

        public override int Pierce
        {
            get
            {
                int additiveBuff = Inventory.GetBuff(BuffType.Additive, StatType.Pierce);
                float multiplicativeBuff = Inventory.GetBuff(BuffType.Multiplicative, StatType.Pierce) / 100f;
                return (int)((base.Pierce + additiveBuff) * multiplicativeBuff);
            }
        }

        protected override int TakeDamage(int damage, int pierce)
        {
            int damageTaken = base.TakeDamage(damage, pierce);
            DoesHUDNeedReprint = true;
            return damageTaken;
        }

        public void GainHpBuff(int value)
        {
            _maxHp += value;
            _hp += value;
        }
        public void LoseHpBuff(int value)
        {
            _maxHp -= value;

            if(_maxHp < _hp)
            {
                _hp = _maxHp;
            }
        }

        public int LoseHpByMaxHpPrecentage(int precentage)
        {
            float damage = (_maxHp * precentage) / 100f;
            TakeDamage((int)damage, 100);

            return (int)damage;
        }

        public void PrintPlayerStatus()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Level:{LevelManager.CurrentLevelNumber}");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"HP:{_hp:0000}/{_maxHp:0000}");

            PrintHpBar();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Gold:{Inventory.Gold}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Keys:{Inventory.Keys}");
            Console.WriteLine($"Potions:{Inventory.Potions}");
            DoesHUDNeedReprint = false;
        }
        public void PrintPlayerStats()
        {
            int cursorRow = Console.CursorTop, cursorColumn = Console.CursorLeft;

            Console.Write("Stats:");
            Console.SetCursorPosition(cursorColumn, cursorRow + 1);
            cursorRow += 1;

            Console.Write($"Damage - {Damage}");
            Console.SetCursorPosition(cursorColumn, cursorRow + 1);
            cursorRow += 1;

            Console.Write($"Armor - {Armor}");
            Console.SetCursorPosition(cursorColumn, cursorRow + 1);
            cursorRow += 1;

            Console.Write($"Pierce - {Pierce}");
            Console.SetCursorPosition(cursorColumn, cursorRow + 1);
            cursorRow += 1;

            Console.Write($"Accuracy - {Accuracy}%");
            Console.SetCursorPosition(cursorColumn, cursorRow + 1);
            cursorRow += 1;

            Console.Write($"Evasion - {Evasion}%");
            Console.SetCursorPosition(cursorColumn, cursorRow + 1);

            Console.Write($"Multihit - {Multihit}%");

        }
        private void PrintHpBar()
        {
            int hpBarLength = 20;

            int hpPrecentage = (int) (100 * (_hp / (float)_maxHp));
            int hpBarPrecentage = (int) (MathF.Ceiling(hpPrecentage / 10f) * 10);
            int hpBarValue = (hpBarLength * hpBarPrecentage) / 100;

            Console.BackgroundColor = ConsoleColor.Red;
            for (int i = 0; i < hpBarValue; i++)
            {
                Console.Write(" ");
            }

            Printer.ColorReset();

            for (int i = 0; i < hpBarLength - hpBarValue; i++)
            {
                Console.Write(" ");
            }

            Console.WriteLine();
            
        }

        protected override void WriteActionText(AttackResult result)
        {
            string text = "";
            ActionTextType type = ActionTextType.General;
            switch (result)
            {
                case AttackResult.Missed:
                    text = "You missed...";
                    type = ActionTextType.CombatNegative;
                    break;

                case AttackResult.Dodged:
                    text = "The enemy dodged your attack...";
                    type = ActionTextType.CombatNegative;
                    break;

                case AttackResult.Hit:
                    text = "You hit an enemy!";
                    type = ActionTextType.CombatPositive;
                    break;
            }

            Printer.AddActionText(type, text);

        }

        public void UsePotion()
        {
            if (Inventory.UsePotion())
            {
                if(_hp == _maxHp)
                {
                    Printer.AddActionText(ActionTextType.General, $"You are at full health...");
                    Inventory.GainPotion();
                    return;
                }

                int hpRestored = HealMaxHpPrecentage(20);
                Printer.AddActionText(ActionTextType.CombatPositive, $"Drinking the potion you healed {hpRestored} HP");
            }
            else
            {
                Printer.AddActionText(ActionTextType.General, $"You have 0 potions...");
            }
            
        }

        private int HealMaxHpPrecentage(int precentage)
        {
            float heal = (_maxHp * precentage) / 100f;

            int hpRestored = (int)heal;
            _hp += hpRestored;

            if(_hp > _maxHp)
            {
                hpRestored = _maxHp - (_hp - hpRestored);
                _hp = _maxHp;
            }

            return hpRestored;
        }
    }
}
