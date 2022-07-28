using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyArray : MonoBehaviour
{
    
    [SerializeField] private List<Transform> enemies = new List<Transform>();
    
    [SerializeField] private float fireSpeed;
    [SerializeField] float fireTime;
    private float _fireWaitTime;
    [SerializeField] private GameObject bulletPrefab;
    private Vector3 _bulletInstantiatePos;

    
    void Start()
    {
        _fireWaitTime = Random.Range(0, fireTime);
    }
    
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        _fireWaitTime -= Time.deltaTime;

        if (_fireWaitTime <= 0)
        {
            if (enemies != null)
            {
                Transform lastEnemy = enemies.Last();
                _bulletInstantiatePos = lastEnemy.position - new Vector3(0, lastEnemy.localScale.y / 2, 0);
                Instantiate(bulletPrefab, _bulletInstantiatePos, Quaternion.identity);
            }

            _fireWaitTime = Random.Range(0, fireTime);
        }
    }
}
