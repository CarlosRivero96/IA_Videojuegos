using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KArrive : MonoBehaviour
{
    public Kinematic character;
    public Kinematic target;
    public float maxSpeed;

    // Satisfaction radius
    public float radius;

    // Time to target
    public float timeToTarget = 0.25f;


    KinematicSteeringOutput getSteering()
    {
        KinematicSteeringOutput result = new KinematicSteeringOutput();

        // Get direction to target
        result.velocity = target.transform.position - character.transform.position;

        // Check if within radius
        if (result.velocity.magnitude < radius)
            //request no steering
            return null;

        // move to target
        result.velocity /= timeToTarget;

        // If too fast, maxSpeed
        if (result.velocity.magnitude > maxSpeed){
            result.velocity.Normalize();
            result.velocity *= maxSpeed;
        }

        // Face in the direction we want to move.
        character.newOrientation(character.transform.rotation.eulerAngles.z, result.velocity);
 
        result.rotation = 0;
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
        character.updateK(getSteering());
    }
}
