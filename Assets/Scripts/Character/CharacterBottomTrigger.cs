using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBottomTrigger : MonoBehaviour
{
    [HideInInspector] public bool onPath;

    /* I count the path player touched because when player pass one path to another,
     * once onTriggerEnter works then onTriggerExit,
     * onPath returns false while player on the path */
    private int pathCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Sea"))
        {
            if(other.CompareTag("Path"))
                pathCount++;

            onPath = true;
        }
        else
            onPath = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            pathCount--;
            if(pathCount == 0)
                onPath = false;
        }
    }
}
