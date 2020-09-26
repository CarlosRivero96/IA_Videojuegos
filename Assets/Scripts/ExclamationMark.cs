using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationMark : MonoBehaviour
{
    GameObject target;
    float startTime;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        GameObject[] miners = GameObject.FindGameObjectsWithTag("Miner");
        target = miners[0];
        foreach(GameObject miner in miners)
        {
            if (Vector3.Distance(gameObject.transform.position, miner.transform.position) < Vector3.Distance(gameObject.transform.position, target.transform.position))
                target = miner;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(target.transform.position.x + 0.7f, target.transform.position.y + 1f, target.transform.position.z);
        
        currentTime = Time.time - startTime;
        if (currentTime > 10)
            Destroy(gameObject);

    }
}
