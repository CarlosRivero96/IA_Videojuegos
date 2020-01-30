using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringOutput
{
    public Vector3 linear;
    public float angular;

    public SteeringOutput(Vector3 linear, float angular)
    {
        this.linear = linear;
        this.angular = angular;
    }
    public SteeringOutput()
    {
        this.linear = Vector3.zero;
        this.angular = 0f;
    }
}
