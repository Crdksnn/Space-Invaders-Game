using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [SerializeField] private float speed;
    private Vector2 _direction = Vector2.down;
    private string _playerTag = "Player";
    private Transform _player;
    private List<Vector2> _playerBoundries = new List<Vector2>();
    void Start()
    {
        if (GameObject.FindGameObjectWithTag(_playerTag))
            _player = GameObject.FindGameObjectWithTag(_playerTag).transform;
    }
    
    void Update()
    {
        ClearPlayerBoundries();
        UpdatePlayerBoundries();
        Movement();
    }

    private bool LineInterSection(Vector2 pos,Vector2 movement)
    {

        for (int i = 0; i < _playerBoundries.Count; i+=2)
        {
            if (Math2d.LineSegmentsIntersection(pos, movement, _playerBoundries[i], _playerBoundries[i + 1]))
            {
                return true;
            }
        }
        
        return false;
    }

    private void UpdatePlayerBoundries()
    {
        
        Vector2 centerPoint = _player.transform.position;
        Vector2 leftUpPoint = new Vector2(_player.position.x - _player.localScale.x / 2, _player.position.y + _player.localScale.y / 2);
        Vector2 leftDownPoint = new Vector2(_player.position.x - _player.localScale.x / 2, _player.position.y - _player.localScale.y / 2);
        Vector2 rightUpPoint = new Vector2(_player.position.x + _player.localScale.x / 2, _player.position.y + _player.localScale.y / 2);
        Vector2 rightDownPoint = new Vector2(_player.position.x + _player.localScale.x / 2, _player.position.y - _player.localScale.y / 2);
        
        Debug.DrawLine(leftUpPoint,rightUpPoint, Color.red);
        Debug.DrawLine(centerPoint, new Vector2(leftUpPoint.x,centerPoint.y), Color.red);
        Debug.DrawLine(centerPoint, new Vector2(rightUpPoint.x,centerPoint.y), Color.red);
        Debug.DrawLine(leftDownPoint,rightDownPoint,Color.red);
        
        _playerBoundries.Add(leftUpPoint);
        _playerBoundries.Add(rightUpPoint);
        
        _playerBoundries.Add(centerPoint);
        _playerBoundries.Add(new Vector2(rightUpPoint.x,centerPoint.y));
        
        _playerBoundries.Add(centerPoint);
        _playerBoundries.Add(new Vector2(rightUpPoint.x,centerPoint.y)); 
        
        _playerBoundries.Add(leftDownPoint);
        _playerBoundries.Add(rightDownPoint);
    }

    private void ClearPlayerBoundries()
    {
        _playerBoundries.Clear();
    }

    private void Movement()
    {
        var pos = (Vector2)transform.position;
        var movement = speed * _direction * Time.deltaTime;
        var newPos = pos + movement;

        Vector2 raycast = (Vector2)transform.position + Vector2.down * transform.localScale.y / 2;
        
        Debug.DrawLine(transform.position, (movement + raycast),Color.red);
        
        if(LineInterSection(transform.position,movement + raycast))
            Destroy(gameObject);
        else
            transform.position = newPos;
        
        Destroy(gameObject, 5f);
    }
}
