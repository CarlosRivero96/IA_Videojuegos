using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    GameObject[] miners;
    float startTime;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        miners = GameObject.FindGameObjectsWithTag("Miner");
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time - startTime;
        if (currentTime > 10)
            Destroy(gameObject);
    }
}
