using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class EnemyArray : MonoBehaviour
{
    
    [SerializeField] private List<Transform> enemyList = new List<Transform>();
    
    //Fire Settings
    [SerializeField] private float fireSpeed;
    [SerializeField] float fireTime;
    private float _fireWaitTime;
    [SerializeField] private GameObject bulletPrefab;
    private Vector3 _bulletInstantiatePos;

    private string _enemiesPositionsString = "Enemies Positions";
    private AllEnimies _enemiesPositions;
    
    void Start()
    {
        //First fire wait time value
        _fireWaitTime = Random.Range(0, fireTime);
        
        _enemiesPositions = GameObject.FindWithTag(_enemiesPositionsString).GetComponent<AllEnimies>();
    }
    
    void Update()
    {
        ClearEnemiesPositions();
        UpdateEnemiesPositions();
        Fire();
        
    }

    private void UpdateEnemiesPositions()
    {
        _enemiesPositions.AddEnemiesList(enemyList);
    }

    private void ClearEnemiesPositions()
    {
        _enemiesPositions.RemoveEnemiesList(enemyList);
    }

    public void RemoveFromlist(Transform enemyObject)
    {
        enemyList.Remove(enemyObject);
        Destroy(enemyObject.gameObject);
    }
    
    private void Fire()
    {
        _fireWaitTime -= Time.deltaTime;

        if (_fireWaitTime <= 0)
        {
            if (enemyList.Count != 0)
            {
                
                Transform firstEnemy = enemyList.First();
                _bulletInstantiatePos = firstEnemy.position - new Vector3(0, firstEnemy.localScale.y / 2, 0);
                Instantiate(bulletPrefab, _bulletInstantiatePos, Quaternion.identity);
            }

            _fireWaitTime = Random.Range(0, fireTime);
        }
    }
}
