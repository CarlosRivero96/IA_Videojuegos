using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSeek : MonoBehaviour
{
    public Kinematic character;
    public Kinematic target;
    public float maxSpeed;
    public bool flee;

    KinematicSteeringOutput getSteering()
    {
        KinematicSteeringOutput result = new KinematicSteeringOutput();

        // Get the direction to the target.
        if (!flee)
            result.velocity = target.transform.position - character.transform.position;
        else
            result.velocity = character.transform.position - target.transform.position;

        // The velocity is along this direction, at full speed.
        result.velocity.Normalize();
        result.velocity *= maxSpeed;

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
