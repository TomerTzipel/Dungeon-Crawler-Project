

using System.Diagnostics;

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
        public bool Attack(CombatEntity defender)
        {
            bool wasDefenderHit = false;

            AttackResult result = SingularAttack(defender);
            Debug.WriteLine(result.ToString());
            //Update Game Status screen

            if(result == AttackResult.Hit)
            {
                wasDefenderHit = true;
            }

            int multihit = Multihit;

            

            while (multihit > 0)
            {
                if (RollChance(multihit))
                {
                    result = SingularAttack(defender);
                    Debug.WriteLine(result.ToString());
                    //Update Game Status screen

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
            if(!RollChance(Accuracy))
            {
                return AttackResult.Missed;
            }

            if (RollChance(defender.Evasion))
            {
                return AttackResult.Dodged;
            }

            defender.TakeDamage(Damage,Pierce);
            return AttackResult.Hit;
        }

        private void TakeDamage(int damage, int pierce)
        {
            int calculatedDamage = CalculateDamage(damage, pierce);
            Debug.WriteLine(calculatedDamage.ToString());
            _hp -= calculatedDamage;

            if (_hp < 0) _hp = 0;
        }

        private int CalculateDamage(int damage,int pierce)
        {
            return damage - (int)(Armor * ((100 - pierce)/100f));
        }

        public void LoseHpByMaxHpPrecentage(int precentage) 
        { 
            float damage = (_maxHp * precentage) / 100f;
            TakeDamage((int)damage,100);
        }
    }
}
