using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
            
        }   
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        bool hasHit = Physics.Raycast(ray, out hit);
        //Debug.DrawRay(lastRay.origin, lastRay.direction * 100);
        
        if (hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hit.point; 
        }   
    }
}
