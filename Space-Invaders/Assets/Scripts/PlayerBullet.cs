using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    [SerializeField] private float speed;
    private Vector2 direction = Vector2.up;
    
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        var pos = (Vector2)transform.position;
        var movement = speed * direction * Time.deltaTime;
        var newPos = pos + movement;

        transform.position = newPos;
        
        Destroy(gameObject, 5f);
    }
}