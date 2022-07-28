using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float fireTime;
    [SerializeField] private GameObject bulletPrefab;
    private float _fireWaitTime;
    private Vector2 bulletInstantiatePos;
    //Boundries
    private Vector2 leftBoundry = new Vector2(-2.5f, 0);
    private Vector2 rightBoundry = new Vector2(2.5f, 0);
    void Start()
    {
        _fireWaitTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Fire();
    }

    private void Fire()
    {

        _fireWaitTime -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            if (_fireWaitTime <= 0)
            {
                bulletInstantiatePos = (Vector2)transform.position + new Vector2(0, transform.localScale.y / 2);
                Instantiate(bulletPrefab, bulletInstantiatePos, Quaternion.identity);
                _fireWaitTime = fireTime;
            }
        }
    }

    private void Movement()
    {
        var pos = (Vector2)transform.position;
        var movement = Vector2.zero;
        
        if(Input.GetKey(KeyCode.A))
            movement = speed * Vector2.left * Time.deltaTime;
        
        if(Input.GetKey(KeyCode.D))
            movement = speed * Vector2.right * Time.deltaTime;
        
        var newPos = pos + movement;

        if (leftBoundry.x <= newPos.x && newPos.x <= rightBoundry.x)
            transform.position = newPos;
    }
}
