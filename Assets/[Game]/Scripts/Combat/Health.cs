using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
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
            }
            else
            {
                return;
            }
        }
    }
}
