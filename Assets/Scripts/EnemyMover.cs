using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Waypoint> _path = new List<Waypoint>();

    private void Start()
    {
        PrintWaypointsName();
    }

    private void PrintWaypointsName()
    {
        foreach (Waypoint waypoint in _path)
        {
            Debug.Log(waypoint.name);
        }
    }
}
