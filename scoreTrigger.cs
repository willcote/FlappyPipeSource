using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameEvents.current.addPoint();
        }
    }
}