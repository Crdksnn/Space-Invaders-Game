using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    [SerializeField] private float speed;
    private Vector2 direction = Vector2.up;
    
    private string _enemiesPositionsString = "Enemies Positions";
    private AllEnimies _allEnemiesList;
    
    private Vector2 _leftUpPoint;
    private Vector2 _leftDownPoint;
    private Vector2 _rightUpPoint;
    private Vector2 _rightDownPoint;
    
    void Start()
    {
        _allEnemiesList = GameObject.FindWithTag(_enemiesPositionsString).GetComponent<AllEnimies>();
    }

    void Update()
    {
        
        Movement();
    }

    private void Movement()
    {
        var pos = (Vector2)transform.position;
        var movement = speed * direction * Time.deltaTime;
        var newPos = pos + movement;

        Vector2 raycast = (Vector2)transform.position + Vector2.up * transform.localScale.y / 2;
        
        Debug.DrawLine(transform.position,raycast + movement, Color.red);
        
        if(LineIntersection(pos,movement + raycast))
            Destroy(gameObject);

        else
            transform.position = newPos;

        
        Destroy(gameObject, 5f);
    }

    private bool LineIntersection(Vector2 pos, Vector2 movement)
    {

        List<List<Transform>> allEnemies = _allEnemiesList.GetEnemiesList();

        for (int i = 0; i < allEnemies.Count; i++)
        {
            for (int j = 0; j < allEnemies[i].Count; j++)
            {

                Transform currentEnemy = allEnemies[i][j];
                float localScale = currentEnemy.transform.localScale.x;

                _leftUpPoint = new Vector2(currentEnemy.position.x - localScale / 2, currentEnemy.position.y + localScale / 2);
                _leftDownPoint = new Vector2(currentEnemy.position.x - localScale / 2, currentEnemy.position.y - localScale / 2);
                _rightUpPoint = new Vector2(currentEnemy.position.x + localScale / 2, currentEnemy.position.y + localScale / 2);
                _rightDownPoint = new Vector2(currentEnemy.position.x + localScale / 2, currentEnemy.position.y - localScale / 2);

                List<Vector2> currentEnemyBoundry = new List<Vector2>();
                
                //Down boundry
                currentEnemyBoundry.Add(_leftDownPoint);
                currentEnemyBoundry.Add(_rightDownPoint);
                
                //Top boundry
                currentEnemyBoundry.Add(_leftUpPoint);
                currentEnemyBoundry.Add(_rightUpPoint);
                
                for (int k = 0; k < currentEnemyBoundry.Count; k += 2)
                {
                    if (Math2d.LineSegmentsIntersection(pos, movement, currentEnemyBoundry[k], currentEnemyBoundry[k + 1]))
                    {
                        allEnemies[i].Remove(currentEnemy);
                        Destroy(currentEnemy.gameObject);
                        return true;
                    }
                        
                }
                
            }
        }

        return false;
    }
    
    
}