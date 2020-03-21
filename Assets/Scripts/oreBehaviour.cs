using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class oreBehaviour : MonoBehaviour
{
    float creationTimer;
    bool change = true;
    public bool active = true;
    public float delay;
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public string activeTag;

    void Start()
    {
        active = true;
        change = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (change && !active)
        {
            creationTimer = Time.time;
            gameObject.tag = "OreInactive";
            this.GetComponent<SpriteRenderer>().sprite = inactiveSprite;
            change = false;
        }
        else if (active || Time.time - creationTimer > delay)
        {
            gameObject.tag = activeTag;
            this.GetComponent<SpriteRenderer>().sprite = activeSprite;
            change = true;
            active = true;
        }
    }
}