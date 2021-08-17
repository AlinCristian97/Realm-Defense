using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Tower.Tower _towerPrefab;
    [SerializeField] private bool _isPlaceable;
    public bool IsPlaceable => _isPlaceable;

    private GridManager _gridManager;
    private Vector2Int _coordinates = new Vector2Int();

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
    }

    private void Start()
    {
        if (_gridManager != null)
        {
            _coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);

            if (!_isPlaceable)
            {
                _gridManager.BlockNode(_coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (_isPlaceable)
        {
            bool wasPlaced = _towerPrefab.CreateTower(_towerPrefab, transform.position);
            
            _isPlaceable = !wasPlaced;
        }
    }
}