using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMatching : MonoBehaviour
{
    public Kinematic character;
    public Kinematic target;
    public float maxAcceleration;
    // Time over which to achieve target speed
    public float timeToTarget = 0.1f;

    SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        // Acceleration tries to get to the target velocity
        result.linear = target.velocity - character.velocity;
        result.linear /= timeToTarget;

        // Check if the acceleration is too fast
        if (result.linear.magnitude > maxAcceleration){
            result.linear.Normalize();
            result.linear *= maxAcceleration;
        }

        result.angular = 0;
        return result;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        character.updateSLinear(getSteering().linear, maxAcceleration);
    }
}
