using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways] // Executes both in Edit & Play Mode
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    Color _defaultColor = Color.white;
    Color _blockedColor = Color.black;

    [SerializeField] private TextMeshPro _label;
    private Vector2Int _coordinates;
    private Waypoint _waypoint;

    // TODO: Why does it throw NullReferenceException?
    // private void Awake()
    // {
    //     _label = GetComponent<TextMeshPro>();
    // }
    
    private void Awake()
    {
        _waypoint = GetComponentInParent<Waypoint>();
        DisplayCoordinates();

        _label.enabled = false;
    }

    void Update()
    {
        if (!Application.isPlaying) // Only executes in Edit Mode
        {
            DisplayCoordinates();
            UpdateObjectName();
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
        if (_waypoint.IsPlaceable)
        {
            _label.color = _defaultColor;
        }
        else
        {
            _label.color = _blockedColor;
        }
    }

    private void DisplayCoordinates()
    {
        _coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        _coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        
        _label.text = $"{_coordinates.x}, {_coordinates.y}";
    }

    void UpdateObjectName()
    {
        transform.parent.name = _coordinates.ToString();
    }
}