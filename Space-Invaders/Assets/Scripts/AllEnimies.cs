using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllEnimies : MonoBehaviour
{
    
    private List<List<Transform>> _allEnemiesList = new List<List<Transform>>();
    
    public void AddEnemiesList(List<Transform> enemiesArray)
    {
        _allEnemiesList.Add(enemiesArray);
    }

    public void RemoveEnemiesList(List<Transform> enemiesArray)
    {
        _allEnemiesList.Remove(enemiesArray);
    }

    public List< List<Transform> > GetEnemiesList()
    {
        return _allEnemiesList;
    }
    
}
