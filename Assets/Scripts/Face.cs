using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : Align
{
    protected Vector3 wanderPosition = Vector3.zero;
    protected SteeringOutput getSteeringFace()
    {
        Vector3 direction;

        // 1. Calculate target to delegate to align
        // Work out direction to target
        if (wanderPosition.magnitude == 0)
            direction = target.transform.position - character.transform.position;
        else
            direction = wanderPosition - character.transform.position;

        // Check for a zero direction, and make no change if so
        if (direction.magnitude == 0)
        {
            SteeringOutput result = new SteeringOutput();
            result.linear = Vector3.zero;
            result.angular = 0;
            return result;
        }

        // 2. Delegate to align
        faceOrientation = (float) (System.Math.Atan2(-direction.x, direction.y) * 180 / System.Math.PI);

        return getSteering();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        character.updateSAngular(getSteeringFace().angular, maxRotation);
    }
}
