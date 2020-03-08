using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWhereYouAreGoing : Align
{
    SteeringOutput getSteeringLook()
    {
        // 1. Calculate the target to delegate to align
        // Check for a zero direction, and make no change if so
        Vector3 velocity = character.velocity;
        if (velocity.magnitude == 0)
        {
            SteeringOutput result = new SteeringOutput();
            character.rotation = 0;
            result.linear = Vector3.zero;
            result.angular = 0;
            return result;
        }

        // Otherwise set the target based on the velocity
        faceOrientation = (float) (System.Math.Atan2(-velocity.x, velocity.y) * 180 / System.Math.PI);

        // 2. Delegate to align
        return getSteering();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        character.updateSAngular(getSteeringLook().angular, maxRotation);
    }
}
