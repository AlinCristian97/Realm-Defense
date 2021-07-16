using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways] // Executes both in Edit & Play Mode
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private TextMeshPro _label;
    private Vector2Int _coordinates;

    
    // TODO: Why does it throw NullReferenceException?
    // private void Awake()
    // {
    //     _label = GetComponent<TextMeshPro>();
    // }
    
    private void Awake()
    {
        DisplayCoordinates();   
    }

    void Update()
    {
        if (!Application.isPlaying) // Only executes in Edit Mode
        {
            DisplayCoordinates();
            UpdateObjectName();
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