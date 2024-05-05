

using System;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace GameSystems
{

    public enum AttackResult
    {
        Missed,Dodged,Hit
    }

    public class CombatEntity
    {
        protected int _hp;
        protected int _maxHp;

        private int _armor;

        private int _damage;
        private int _evasion; // precentage
        private int _accuracy; // precentage
        private int _multihit; // positive
        private int _pierce;// precentage

        public bool IsAlive
        {
            get { return _hp > 0; }
        }

        public virtual int Armor
        {
            get { return _armor; }
        }

        public virtual int Evasion
        {
            get { return _evasion; }
        }

        public virtual int Accuracy
        {
            get { return _accuracy; }
        }

        public virtual int Multihit
        {
            get { return _multihit; }
        }

        public virtual int Pierce
        {
            get { return _pierce; }
        }

        public virtual int Damage
        {
            get { return _damage; }
        }

        public CombatEntity(int maxHp, int damage,int evasion, int accuracy, int multihit, int armor, int pierce)
        {
            _maxHp = maxHp;
            _hp = maxHp;

            _damage = damage;
            _accuracy = accuracy;
            _evasion = evasion;
            _multihit = multihit;
            _armor = armor;
            _pierce = pierce;
        }

        public void ScaleStats(float precentage)
        {
            _maxHp = (int)(_maxHp * precentage);
            _hp = _maxHp;

            _damage = (int)(_damage * precentage);
            _accuracy = (int)(_accuracy * precentage);
            _evasion = (int)(_evasion * precentage);
            _multihit = (int)(_multihit * precentage);
            _armor = (int)(_armor * precentage);
            _pierce = (int)(_pierce * precentage);

        }

        public bool Attack(CombatEntity defender)
        {
            bool wasDefenderHit = false;

            AttackResult result = SingularAttack(defender);
            WriteActionText(result);

            if (result == AttackResult.Hit)
            {
                wasDefenderHit = true;
            }

            int multihit = Multihit;

            

            while (multihit > 0)
            {
                if (RollChance(multihit))
                {
                    result = SingularAttack(defender);
                    WriteActionText(result);

                    if (!wasDefenderHit && result == AttackResult.Hit)
                    {
                        wasDefenderHit = true;
                    }

                }

                multihit -= 100;
            }

            return wasDefenderHit;

        }
        private AttackResult SingularAttack(CombatEntity defender)
        {

            if (!RollChance(Accuracy))
            {
                return AttackResult.Missed;
            }

            if (RollChance(defender.Evasion))
            {
                return AttackResult.Dodged;
            }

            int damageTaken = defender.TakeDamage(Damage,Pierce);
            return AttackResult.Hit;
        }

        protected virtual int TakeDamage(int damage, int pierce)
        {
            int calculatedDamage = CalculateDamage(damage, pierce);
            if(calculatedDamage <= 0)
            {
                return 0;
            }

            _hp -= calculatedDamage;
            if (_hp < 0) _hp = 0;

            return calculatedDamage;
        }

        private int CalculateDamage(int damage,int pierce)
        {
            return damage - (int)(Armor * ((100 - pierce)/100f));
        }

        protected virtual void WriteActionText(AttackResult result)
        {
            string text = "";
            ActionTextType type = ActionTextType.General;
            switch (result)
            {
                case AttackResult.Missed:
                    text = "The enemy attack missed!";
                    type = ActionTextType.CombatPositive;
                    break;

                case AttackResult.Dodged:
                    text = "You dodged an attack!";
                    type = ActionTextType.CombatPositive;
                    break;

                case AttackResult.Hit:
                    text = "You were hit!";
                    type = ActionTextType.CombatNegative;
                    break;
            }

            Printer.AddActionText(type,text);
        }
    }
}
