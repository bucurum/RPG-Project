using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using RPG.Saving;

namespace RPG.Combat
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoint = 100f;
        private bool isDeath;
        public bool IsDead()
        {
            return isDeath;
        }

        public void takeDamage(float damage)
        {
            healthPoint = Mathf.Max(healthPoint - damage, 0); //this provides the healthPoint to down below to zero.
            Debug.Log(healthPoint);
            if (healthPoint == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (!isDeath)
            {
                GetComponent<Animator>().SetTrigger("die");
                isDeath = true;
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
            else
            {
                return;
            }
        }

        public object CaptureState()
        {
            return healthPoint;
        }
        public void RestoreState(object state)
        {
            healthPoint = (float)state;
            if (healthPoint == 0)
            {
                Die();
            }
        }
    }
}
