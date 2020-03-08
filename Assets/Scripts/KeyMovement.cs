using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMovement : MonoBehaviour
{

    public float maxSpeed;
    public Kinematic character;
    Vector3 velocity;
    KinematicSteeringOutput result = new KinematicSteeringOutput();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocity.x -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            velocity.x += 1;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            velocity.y += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            velocity.y -= 1;
        }
        
        velocity.Normalize();
        velocity *= maxSpeed;
        if (Input.GetKey(KeyCode.LeftControl))
            result.velocity = velocity*1.5f;
        else
            result.velocity = velocity;
        result.rotation = 0;
        character.updateK(result);

        character.newOrientation(transform.rotation.eulerAngles.z, velocity);
        
    }
}