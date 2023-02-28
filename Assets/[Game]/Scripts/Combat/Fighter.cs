using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour , IAction
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;
        Mover mover;
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
            }
        }

        private bool GetsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }
    }
}
