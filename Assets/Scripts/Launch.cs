using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public Projectile projectile;
    public Kinematic character;
    // Start is called before the first frame update
    void Start()
    {
        projectile.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)){
            projectile.velocity = character.velocity.normalized * 5;
            projectile.velocity += new Vector3(0,0,10);
            Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, 0.01f), Quaternion.identity);
        }
    }
}
