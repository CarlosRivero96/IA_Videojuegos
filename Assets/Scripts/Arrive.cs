using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour
{
    public Kinematic character;
    public Kinematic target;
    public float maxAcceleration;
    public float maxSpeed;

    // Radius for arriving at the target
    public float targetRadius;
    // Radius for slow down
    public float slowRadius;
    // Time to target
    private float timeToTarget = 0.1f;


    SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();
        Vector3 direction;
        float distance;
        float targetSpeed;
        Vector3 targetVelocity;

        // Get direction to target
        direction = target.transform.position - character.transform.position;
        distance = direction.magnitude;

        // Check if within radius, request no steering
        if (distance < targetRadius){
            character.velocity = new Vector3(0,0,0);
            result.linear = new Vector3(0,0,0);
            result.angular = 0;
            return result;
        }

        // If we outside slowRadius, move at maxSpeed
        else if (distance > slowRadius)
            targetSpeed = maxSpeed;
        
        else
            targetSpeed = maxSpeed * distance / slowRadius;

        // Target velocity combines speed and direction
        targetVelocity = direction;
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        // Acceleration tries to get to the target velocity
        result.linear = targetVelocity - character.velocity;
        result.linear /= timeToTarget;

        // Check if acceleration too fast
        if (result.linear.magnitude > maxAcceleration){
            result.linear = targetVelocity - character.velocity;
            result.linear *= maxAcceleration;
        }

        result.angular= 0;
        return result;
    }
    
    // Awake is called first
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
        character.updateSLinear(getSteering().linear, maxSpeed);
    }
}
