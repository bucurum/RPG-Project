using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
            }
            //TODO: When the player trigger make the player unmoveable or stand where the player is on current position
        }
    }
}
