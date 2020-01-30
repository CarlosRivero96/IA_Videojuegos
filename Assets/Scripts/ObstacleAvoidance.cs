using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : Seek
{
    // Minimum distance to a wall (how far to avoid collision)
    // Should be greater than the radius of the character
    public float avoidDistance;
    // Distance to look ahead for a collision
    // (The length of the collision ray)
    public float lookahead;

    // Obstacle list
    public List<GameObject> obstacles = new List<GameObject>();

    SteeringOutput getSteeringAvoid()
    {
        Vector3 ray;
        // 1. Calculate the target to delegate to seek
        // Calculate the collision ray vector.
        ray = character.velocity;
        ray.Normalize();
        ray *= lookahead;

        // Find the collision
        Collision collision = getCollision(character.transform.position, ray);

        // If have no collision, do nothing
        if (collision == null)
        {
            SteeringOutput result = new SteeringOutput();
            result.linear = Vector3.zero;
            result.angular = 0f;
            return result;
        }
        // 2. Otherwise create a target and delegate to seek
        obs = -(collision.position + collision.normal * avoidDistance);
        return getSteering();

    }

    Collision getCollision(Vector3 position, Vector3 moveAmount)
    {
        Collision retorno = new Collision();
        Vector3 normal = Vector3.zero;
        Vector3 rayPosition = position + moveAmount;
        foreach(GameObject obstacle in obstacles)
        {
            if (rayPosition.x >= (obstacle.transform.position.x - obstacle.transform.localScale.x/2 - 1) &&
            rayPosition.x <= (obstacle.transform.position.x + obstacle.transform.localScale.x/2 + 1) &&
            rayPosition.y >= (obstacle.transform.position.y - obstacle.transform.localScale.y/2 - 1) &&
            rayPosition.y <= (obstacle.transform.position.y + obstacle.transform.localScale.y/2 + 1))
            {
                Debug.Log(obstacle.transform.position);
                col = true;
                normal = obstacle.transform.position - position;
                retorno = new Collision(rayPosition, normal);
                return retorno;
            }
        };
        col = false;
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        obstacles.Clear();
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("Obstacle"))
            obstacles.Add(o);

        character.updateSLinear(getSteeringAvoid().linear, maxSpeed);
    }
}