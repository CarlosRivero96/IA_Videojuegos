using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour
{
    public Kinematic character;
    public Kinematic target;
    public float maxAngularAcceleration;
    public float maxRotation;
    // the radius for arriving at the target
    public float targetRadius;
    // the radius for beginning to slow down
    public float slowRadius;
    // The timeover which to achieve target speed
    private float timeToTarget = 0.1f;
    protected float faceOrientation = 0;

    protected SteeringOutput getSteering(){
        SteeringOutput result = new SteeringOutput();
        float rotation;
        float rotationSize;
        float targetRotation;

        // Get the naive direction to the target
        if (faceOrientation == 0)
            rotation = target.transform.rotation.eulerAngles.z - character.transform.rotation.eulerAngles.z;
        else
            rotation = faceOrientation - character.transform.rotation.eulerAngles.z;

        // Map the result to the ()
        rotation = MapToRange(rotation);
        rotationSize = System.Math.Abs(rotation);

        // Check if we are there, return no steering
        if (rotationSize < targetRadius){
            result.angular = 0f;
            result.linear = new Vector3(0,0,0);
            character.rotation = 0f;
            return result;
        }
        
        // If we are outside the slowRadius, max rotation
        if (rotationSize > slowRadius)
            targetRotation = maxRotation;

        // Otherwise calculate a scaled rotation
        else{
            targetRotation = maxRotation * rotationSize / slowRadius;
        }

        // Final targetRotation combines speed and direction
        targetRotation *= rotation / rotationSize;

        // Acceleration tries to get to the targetRotation
        result.angular = targetRotation - character.rotation;
        result.angular /= timeToTarget;

        // Check if acceleration is too big
        float angularAcceleration = System.Math.Abs(result.angular);
        if (angularAcceleration > maxAngularAcceleration){
            result.angular /= angularAcceleration;
            result.angular *= maxAngularAcceleration;
        }

        result.linear = new Vector3(0,0,0);
        return result;
    }
    float MapToRange(float angle)
    {
        if (angle > 180)
            angle -= 360;
        else if (angle < -180)
            angle += 360;
        return angle;
    }

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        character.updateSAngular(getSteering().angular, maxRotation);
    }
}