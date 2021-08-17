using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEngine;

[ExecuteAlways] // Executes both in Edit & Play Mode
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    Color _defaultColor = Color.white;
    Color _blockedColor = Color.black;
    Color _exploredColor = Color.yellow;
    Color _pathColor = new Color(1f, 0.5f, 0f);

    [SerializeField] private TextMeshPro _label;
    private Vector2Int _coordinates;

    private GridManager _gridManager;

    // TODO: Why does it throw NullReferenceException?
    // private void Awake()
    // {
    //     _label = GetComponent<TextMeshPro>();
    // }
    
    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        DisplayCoordinates();

        _label.enabled = false;
    }

    void Update()
    {
        if (!Application.isPlaying) // Only executes in Edit Mode
        {
            DisplayCoordinates();
            UpdateObjectName();
            _label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _label.enabled = !_label.IsActive();
        }
    }

    private void SetLabelColor()
    {
        if (_gridManager == null) { return; }

        Node node = _gridManager.GetNode(_coordinates);

        if (node == null) { return; }
        
        if (!node.IsWalkable)
        {
            _label.color = _blockedColor;
        }
        else if (node.IsPath)
        {
            _label.color = _pathColor;
        }
        else if (node.IsExplored)
        {
            _label.color = _exploredColor;
        }
        else
        {
            _label.color = _defaultColor;
        }
    }

    private void DisplayCoordinates()
    {
        if (_gridManager == null) return;
        
        _coordinates.x = Mathf.RoundToInt(transform.parent.position.x / _gridManager.UnityGridSize);
        _coordinates.y = Mathf.RoundToInt(transform.parent.position.z / _gridManager.UnityGridSize);
        
        _label.text = $"{_coordinates.x}, {_coordinates.y}";
    }

    void UpdateObjectName()
    {
        transform.parent.name = _coordinates.ToString();
    }
}