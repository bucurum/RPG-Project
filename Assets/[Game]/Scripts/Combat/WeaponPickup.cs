using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("loggg");
                other.GetComponent<Fighter>().EquipWeapon(weapon);  
                Destroy(gameObject);
            }
        }
    }
}
