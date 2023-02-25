using System;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void MoveToCursor()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            //Debug.DrawRay(lastRay.origin, lastRay.direction * 100);

            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        void Update()
        {
            InteractWithMovement();
            InteractWithCombat();
        }

        private void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                
            }
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(1))
            {
                MoveToCursor();
            }
        }
    }
}
