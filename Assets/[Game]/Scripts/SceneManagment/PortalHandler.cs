using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalHandler : MonoBehaviour
{
    [SerializeField] bool isNextLevelPortal;
    int nextScene;
    int previousScene;
    void Start()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        previousScene = SceneManager.GetActiveScene().buildIndex - 1;
    }
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            if (isNextLevelPortal)
            {
                SceneManager.LoadScene(nextScene);
            }else
            {
                SceneManager.LoadScene(previousScene);
            } 
        }
        
    }
}
