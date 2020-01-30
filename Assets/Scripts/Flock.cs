    // using System.Collections;
    // using System.Collections.Generic;
    // using UnityEngine;

    // public class Flock : MonoBehaviour
    // {
    //     public Kinematic characterFace;
    //     public Kinematic targetFace;
    //     public float maxAngularAccelerationFace;
    //     public float maxRotationFace;
    //     // the radius for arriving at the target
    //     public float targetRadius;
    //     // the radius for beginning to slow down
    //     public float slowRadius;

    //     public Kinematic characterSeek;
    //     public Kinematic targetSeek;
    //     public float maxAccelerationSeek;
    //     public float maxSpeedSeek;

    //     public float maxAcceleration;
    //     public float maxRotation;
    //     Face face;
    //     Seek seek;

    //     void Start()
    //     {
    //         face = new Face(characterFace, targetFace, maxAngularAccelerationFace, maxRotationFace,
    //         targetRadius, slowRadius);
            
    //         seek = new Seek(characterSeek, targetSeek, maxAccelerationSeek, maxSpeedSeek);
    //     }

    //     SteeringOutput getSteering()
    //     {
    //         SteeringOutput result = new SteeringOutput();

    //         // Accumulate accelerations
    //         result += face.getSteeringFace();
    //         result += seek.getSteering();

    //         if (result.linear.normalized > maxAcceleration)
    //         {        result.linear.Normalize();
    //             result.linear *= maxAcceleration;
    //         }
    //     }

    //     void Update()
    //     {
    //         characterFace.updateS(getSteering(), maxSpeedSeek);
    //     }

    // }