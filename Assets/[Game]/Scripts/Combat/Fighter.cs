using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
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
            target = combatTarget.transform;
        }
        void Update()
        {
            bool isInRange = Vector3.Distance(transform.position , target.position) < weaponRange;
            if (target != null && !isInRange)
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Stop();
            }
        }
    }
}
