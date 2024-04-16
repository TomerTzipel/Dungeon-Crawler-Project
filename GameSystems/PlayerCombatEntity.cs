using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystems
{
    public class PlayerCombatEntity : CombatEntity
    {
        private Inventory _inventory;

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

    }
}
