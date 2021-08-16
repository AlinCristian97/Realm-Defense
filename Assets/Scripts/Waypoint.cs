using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Tower.Tower _towerPrefab;
    [SerializeField] private bool _isPlaceable;
    public bool IsPlaceable => _isPlaceable;

    private void OnMouseDown()
    {
        if (_isPlaceable)
        {
            bool wasPlaced = _towerPrefab.CreateTower(_towerPrefab, transform.position);
            
            _isPlaceable = !wasPlaced;
        }
    }
}