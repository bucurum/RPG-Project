using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 2f;
        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;
        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            guardPosition = transform.position;
            mover = GetComponent<Mover>();
        }
        void Update()
        {
            if (health.IsDead())
            {
                return;
            }
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if(timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                GuardBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            mover.StartMoveAction(guardPosition);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            Debug.Log(gameObject.name + " is chasing");
            fighter.Attack(player);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }
        private void OnDrawGizmosSelected() // Called by unity gizmos
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }

}