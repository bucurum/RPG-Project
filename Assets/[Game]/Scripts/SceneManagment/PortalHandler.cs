using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PortalHandler : MonoBehaviour
{
    // enum DestinationIdentifier
    // {
    //     A,B,C,D,E
    // }
    //[SerializeField] bool isNextLevelPortal;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int buildIndex = -1;
    //[SerializeField] DestinationIdentifier destination;
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
        DontDestroyOnLoad(gameObject);
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
            if (portal == this) continue;

            return portal;
       }
       return null;
    }



    // private void UpdatePLayer(PortalHandler otherPortal)
    // {
    //     GameObject player = GameObject.FindWithTag("Player");
    //     player.GetComponent<NavMeshAgent>().enabled = false;
    //     player.transform.position = otherPortal.spawnPoint.position;
    //     player.transform.rotation = otherPortal.spawnPoint.rotation;
    //     player.GetComponent<NavMeshAgent>().enabled = true;
    // }
    // private void UpdatePlayer(PortalHandler otherPortal)
    //     {

    //         GameObject player = GameObject.FindWithTag("Player");

    //         player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
    //         player.transform.rotation = otherPortal.spawnPoint.rotation;


    //     }

    // private PortalHandler GetOtherPortal()
    // {
    //     foreach (PortalHandler portal in FindObjectsOfType<PortalHandler>())
    //     {
    //         if (portal == this) continue;
    //         if(portal.destination != destination) continue;

    //         if(portal != this)
    //         {
    //             print("Other Portal Found");
    //             return portal;
    //         }
    //     }
    //     return null;
    // }
}
