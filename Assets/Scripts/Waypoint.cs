using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private bool _isPlaceable;
    
    private void OnMouseDown()
    {
        if (_isPlaceable)
        {
            Debug.Log(transform.name);
        }
    }
}