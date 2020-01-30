using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KWander : MonoBehaviour
{
    public Kinematic character;
    public float maxSpeed;
    
    // Maximum rotation speed
    public float maxRotation;

    KinematicSteeringOutput GetSteering()
    {
        KinematicSteeringOutput result = new KinematicSteeringOutput();

        // Get velocity from vector form of orientation
        result.velocity = maxSpeed * AsVector(character.transform.rotation.eulerAngles.z);

        // Change orientation randomly
        result.rotation = randomBinomial() * maxRotation;
        
        //result.rotation = 0;
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

    float randomBinomial()
    {
        return Random.Range(-1.0f, 1.0f);
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
        character.updateK(GetSteering());
    }
}