using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;
        static bool hasSpawned = false;

        void Awake()
        {
            if (hasSpawned)
            {
                return;
            }
            else
            {
                SpawnPersistentObject();
            }

        }

        private void SpawnPersistentObject()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab, transform.position, Quaternion.identity);
            DontDestroyOnLoad(persistentObject);
            hasSpawned = true;
        }
    }

}