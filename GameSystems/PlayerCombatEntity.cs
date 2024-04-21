using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystems
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
            Console.WriteLine($"Level:{LevelManager.CurrentLevelNumber}");

            Console.Write($"HP:{_hp:0000}/{_maxHp:0000}");

            PrintHpBar();

            Console.WriteLine($"Gold:{Inventory.Gold}");
            Console.WriteLine($"Keys:{Inventory.Keys}");
            Console.WriteLine($"Potions:{Inventory.Potions}");
            DoesHUDNeedReprint = false;
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
    }
}
