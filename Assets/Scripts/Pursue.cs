using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : Seek
{
    // maximum prediction time
    public float maxPrediction;

    SteeringOutput getSteeringPursue()
    {
        Vector3 direction;
        float distance;
        float speed;
        float prediction;

        // 1. Calculate the target to delegate to seek
        // Work out the distance to target
        direction = target.transform.position - character.transform.position;
        distance = direction.magnitude;

        // Work out our current speed
        speed = character.velocity.magnitude;

        // Check if speed gives a reasonable prediction time
        if (speed <= distance / maxPrediction)
            prediction = maxPrediction;
        else
            prediction = distance / speed;

        // Put the target together
        future = target.velocity * prediction;

        // 2. Delegate to seek
        return getSteering();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        character.updateSLinear(getSteeringPursue().linear, maxAcceleration);
    }
}
