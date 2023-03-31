using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagment
{
    public class PortalHandler : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A,B,C,D,E
        }
        [SerializeField] bool isNextLevelPortal;
        [SerializeField] Transform spawnPoint;
        [SerializeField] int buildIndex = -1;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 1f;
        //int nextScene;
        //int previousScene;
        // void Start()
        // {
        //     nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        //     previousScene = SceneManager.GetActiveScene().buildIndex - 1;
        // }
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {

            if (buildIndex < 0)
            {
                Debug.LogError("Scene to load not set");
                yield break;
            }

            Fader fader = FindObjectOfType<Fader>();

            DontDestroyOnLoad(gameObject);

            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(buildIndex);
            
            // if (isNextLevelPortal)
            // {
            //     yield return SceneManager.LoadSceneAsync(nextScene);// wait for complete the scene loading
            // }else
            // {
            //     yield return SceneManager.LoadSceneAsync(previousScene);// wait for complete the scene loading
            // }
            
            PortalHandler otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            
            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime); 

            Destroy(gameObject);
        }

        private void UpdatePlayer(PortalHandler otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private PortalHandler GetOtherPortal()
        {
           foreach(PortalHandler portal in FindObjectsOfType<PortalHandler>()) 
           {
                if(portal == this) continue;
                if(portal.destination != destination) continue;

                return portal;
           }
           return null;
        }
    }
}