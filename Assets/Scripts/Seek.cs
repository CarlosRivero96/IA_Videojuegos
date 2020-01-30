using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    public Kinematic character;
    public Kinematic target;
    public float maxAcceleration;
    public float maxSpeed;
    public bool flee = false;
    public bool col = false;
    protected Vector3 future = Vector3.zero;
    protected Vector3 obs = Vector3.zero;

    protected SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();
        // Get the direction to the target.
        if (!col)
        {
            if (!flee)
                result.linear = target.transform.position - character.transform.position;
            else
                result.linear = character.transform.position - target.transform.position;
            result.linear += future;
        }
        else
        {
            result.linear = obs - character.transform.position;
        }

        // The velocity is along this direction, at full speed.
        result.linear.Normalize();
        result.linear *= maxAcceleration;

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
        character.updateSLinear(getSteering().linear, maxSpeed);
    }
}