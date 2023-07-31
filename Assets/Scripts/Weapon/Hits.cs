using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hits : MonoBehaviour
{
    [SerializeField] private float timeToDestroyHit;

    void Start()
    {
        StartCoroutine(waitForDestroy(timeToDestroyHit));
    }

    IEnumerator waitForDestroy(float time) //Coroutine for destroy objects
    {
        yield return new WaitForSeconds(time);
        DestroyImmediate(gameObject);
    }
}
