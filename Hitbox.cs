using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    //checks if npc got into danger zone
    private NPController npc;

    private void Start()
    {
        npc = GetComponentInParent<NPController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Danger"))
        {
            Debug.Log("triggered");

            npc.DangerTrigger(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
}
