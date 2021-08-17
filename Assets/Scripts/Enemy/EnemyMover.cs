using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyMover : MonoBehaviour
    {
        private List<Tile> _path = new List<Tile>();
        [SerializeField] [Range(0f, 5f)] private float _speed = 1f;

        private Enemy _enemy;
    
        private void OnEnable()
        {
            FindPath();
            ReturnToStart();
            StartCoroutine(FollowPath());
        }

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
        }

        private void FindPath()
        {
            _path.Clear();
        
            var parent = GameObject.FindGameObjectWithTag("Path");

            foreach (Transform child in parent.transform)
            {
                var waypoint = child.GetComponent<Tile>();

                if (waypoint != null)
                {
                    _path.Add(waypoint);
                }
            }
        }

        private void ReturnToStart()
        {
            transform.position = _path[0].transform.position;
        }

        private void FinishPath()
        {
            _enemy.StealGold();
            gameObject.SetActive(false);
        }

        private IEnumerator FollowPath() 
        {
            foreach (Tile waypoint in _path)
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

            FinishPath();
        }
    }
}