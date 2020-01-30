using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic : MonoBehaviour
{
    public Vector3 velocity;
    public float rotation;
    Vector3 vz = new Vector3(0f,0f,0f);

    public void updateK(KinematicSteeringOutput steering)
    {
        // Update the position and orientation.
        transform.position += steering.velocity * Time.deltaTime;
        vz.z = transform.rotation.eulerAngles.z + steering.rotation;

        // set object rotation
        transform.rotation = Quaternion.Euler(vz);

        velocity = steering.velocity;
    }

    public void updateS(SteeringOutput steering, float maxSpeed)
    {
        // Update the position and orientation.
        transform.position += velocity * Time.deltaTime;
        vz.z += (float) (rotation * Time.deltaTime * 180 / System.Math.PI);
        transform.rotation = Quaternion.Euler(vz);

        // and the velocity and rotation.
        velocity += steering.linear * Time.deltaTime;
        rotation += steering.angular * Time.deltaTime;

        // Check for speeding and clip
        if (velocity.magnitude > maxSpeed){
            velocity.Normalize();
            velocity *= maxSpeed;
        }
    }

    public void updateSLinear(Vector3 linear, float maxSpeed)
    {
        transform.position += velocity * Time.deltaTime;
        velocity += linear * Time.deltaTime;
        if (velocity.magnitude > maxSpeed){
            velocity.Normalize();
            velocity *= maxSpeed;
        }
    }

    public void updateSAngular(float angular, float maxSpeed)
    {
        vz.z += (float) (rotation * Time.deltaTime * 180 / System.Math.PI);
        transform.rotation = Quaternion.Euler(vz);
        rotation += angular * Time.deltaTime;
    }

    public void newOrientation(float current, Vector3 velocity)
    {
        Vector3 orientation = transform.rotation.eulerAngles;
        // Make sure we have a velocity.
        if (velocity.magnitude > 0){
            //Calculate orientation from the velocity.
            orientation.z = (float) (System.Math.Atan2( -velocity.x, velocity.y) * 180 / System.Math.PI);
            transform.rotation = Quaternion.Euler(orientation);
        }
        //Otherwise use the current orientation.
    }
    void Update(){
        if (transform.position.x >= 26 || transform.position.x <= -26)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.y >= 11 || transform.position.y <= -11)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
        }
    }
}
