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
        Transform target;
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
            target = combatTarget.transform;
        }
        public void Cancel()
        {
            target = null;
        }
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null)
            {
                return;
            }

            if (!GetsInRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Cancel();
                
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
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
            health = target.GetComponent<Health>();
            health.takeDamage(DamageAmounth);
        }
        private bool GetsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        
    }
}
