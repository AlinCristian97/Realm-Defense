using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Waypoint> _path = new List<Waypoint>();
    [SerializeField] private float _waitTime = 1f; 

    private void Start()
    {
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath() 
    {
        foreach (Waypoint waypoint in _path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}