using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 gravity = new Vector3(0, 0, 9.81f);

    void Update()
    {
        if (transform.position.z < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            transform.localScale = new Vector3(1,1,1);
            velocity = Vector3.zero;
        }
        else if (transform.position.z != 0)
        {
            transform.position += velocity * Time.deltaTime + (gravity * Time.deltaTime * Time.deltaTime/2);
            velocity += gravity * Time.deltaTime;

            if(velocity.z > 0f)
                transform.localScale += new Vector3(0.01f,0.01f,0);
            else if (velocity.z != 0)
                transform.localScale -= new Vector3(0.01f,0.01f,0);
        }
    }
}