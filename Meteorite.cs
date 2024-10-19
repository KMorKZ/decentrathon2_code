using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    //exclamation zone
    [SerializeField] private float timeToVanish = 5f;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToVanish) { 
            Destroy(this.gameObject);
        }
    }
}
