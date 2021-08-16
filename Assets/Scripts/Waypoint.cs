using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private bool _isPlaceable;
    
    private void OnMouseDown()
    {
        if (_isPlaceable)
        {
            Instantiate(_towerPrefab, transform.position, Quaternion.identity);
            _isPlaceable = false;
        }
    }
}