using System;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        void Update()
        {
            if(InteractWithCombat()) return; 
            if(InteractWithMovement()) return;
        }

        private bool InteractWithCombat() //we set this to bool because when player over hover the enemy it tell us the object is interactable or not
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null)
                {
                    continue;
                }
                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) //if we cant attack to target
                {
                    continue;
                }
                
                if (Input.GetMouseButton(1))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement() // we set this to bool because when player over hover the egde of the world or cant reachable point in the world it tell us the point is reachable or not
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            //Debug.DrawRay(lastRay.origin, lastRay.direction * 100);

            if (hasHit)
            {
                if (Input.GetMouseButton(1))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }
                return true;
                
            }
            return false;
        }
    }
}
