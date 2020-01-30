using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Face
{
    // Radius and forward offset of the wander circle
    public float wanderOffset;
    public float wanderRadius;

    // Maximum rate of which the wander orientation can change
    public float wanderRate;

    // Current orientation of the wander target
    private float wanderOrientation;

    //Maximum acceleration of the character
    public float maxAcceleration;

    SteeringOutput getSteeringWander()
    {
        float targetOrientation;
        SteeringOutput result = new SteeringOutput();

        // 1. Calculate the target to delegate to face
        // Update the wander orientation
        wanderOrientation += Random.Range(-1.0f, 1.0f) * wanderRate;

        // Calculate the combined target orientation
        targetOrientation = wanderOrientation + character.transform.rotation.eulerAngles.z;
        
        // Calculate the center of the wander circle.
        wanderPosition = character.transform.position + wanderOffset * AsVector(character.transform.rotation.eulerAngles.z);

        // Calculate the target location
        wanderPosition += wanderRadius * AsVector(targetOrientation);

        // 2. Delegate to face
        result = getSteeringFace();

        // 3. Now set the linear acceleration to be at full acceleration
        // in the direction of the orientation
        result.linear = maxAcceleration * AsVector(character.transform.rotation.eulerAngles.z);
        
        //return it
        return result;
    }

    Vector3 AsVector(float orientation)
    {
        Vector3 result = new Vector3();
        result.x = - (float) System.Math.Sin(orientation / 180 * System.Math.PI);
        result.y = (float) System.Math.Cos(orientation / 180 * System.Math.PI);
        result.z = 0f;
        return result;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        character.updateS(getSteeringWander(), maxAcceleration);
    }
}
