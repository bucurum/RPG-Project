using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour , IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks =1f;
        [SerializeField] float DamageAmounth = 5;
        Health target;
        Mover mover;
        Health health;
        float timeSinceLastAttack = 0;

        void Awake()
        {
            mover = GetComponent<Mover>();
        }
        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }
        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null)
            {
                return;
            }
            if(target.IsDead())
            {
                return;
            }

            if (!GetsInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                //This will trigger the Hit() Event
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
                
            }
            
        }
        //Animation Event
        void Hit()
        {
            target.takeDamage(DamageAmounth);
        }
        private bool GetsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }
        
        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        
    }
}
