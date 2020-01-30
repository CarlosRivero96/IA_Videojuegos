using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : MonoBehaviour
{
    public Kinematic character;
    public float maxAcceleration;
    // List of potential targets.
    public List<Kinematic> targets = new List<Kinematic>();
    // threshold to take action.
    public float threshold;
    // the constant coefficient of decay for the inverse square law
    public float decayCoefficient;

    SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();
        Vector3 direction = new Vector3();
        float distance = 0f;
        float strength;

        // Loop through each target.
        //for (int t=0 ; t<targets.size ; t++)
        targets.ForEach( target =>
        {
            // Check if target is close
            direction = target.transform.position - character.transform.position;
            distance = direction.magnitude;

            if (distance < threshold)
            {
                // Calculate the strength of repulsion
                // (here using the inverse square law)
                strength = System.Math.Min(decayCoefficient / (distance * distance), maxAcceleration);

                // Add the acceleration
                direction.Normalize();
                result.linear += strength * direction;
            }
            else
            {
                result.linear = Vector3.zero;
            }
        });

        return result;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        character.updateSLinear(-getSteering().linear, maxAcceleration);
    }
}
