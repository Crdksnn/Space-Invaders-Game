using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArraySettings : MonoBehaviour
{
    [SerializeField] private float _arraySpeed;
    private Vector2 target = new Vector2(-.5f,0);
    
    void Update()
    {
        
        BoundryUpdate();
        
    }

    private void BoundryUpdate()
    {

        if (transform.position.x == target.x)
        {
            target.x = -target.x;
            transform.position -= new Vector3(0,.5f,0);
        }
        
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, transform.position.y),_arraySpeed * Time.deltaTime);
        }
        

    }
    
}
