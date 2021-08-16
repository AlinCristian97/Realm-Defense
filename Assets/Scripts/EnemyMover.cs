using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Waypoint> _path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] private float _speed = 1f; 

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
            
            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * _speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}