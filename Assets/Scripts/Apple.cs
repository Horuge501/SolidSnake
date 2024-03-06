using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour 
{
    public BoxCollider2D spawnArea;

    void Start() 
    {
        RandomPos();
    }

    public void RandomPos()
    {
        Bounds bounds = spawnArea.bounds;

        float randX = Random.Range(bounds.min.x, bounds.max.x);
        float randY = Random.Range(bounds.min.y, bounds.max.y);
        transform.position = new Vector3(Mathf.Round(randX), Mathf.Round(randY), 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player") 
        {
            RandomPos();
        }
    }
}



