using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] [Range(0f, 5f)] private float _speed = 1f;

        private List<Node> _path = new List<Node>();
        private Enemy _enemy;
        private GridManager _gridManager;
        private Pathfinder _pathfinder;
    
        private void OnEnable()
        {
            FindPath();
            ReturnToStart();
            StartCoroutine(FollowPath());
        }

        private void Awake()
        {           
            _enemy = GetComponent<Enemy>();
            _gridManager = FindObjectOfType<GridManager>();
            _pathfinder = FindObjectOfType<Pathfinder>();
        }

        private void Start()
        {
            
        }

        private void FindPath()
        {
            _path.Clear();
            _path = _pathfinder.GetNewPath();

        }

        private void ReturnToStart()
        {
            transform.position = _gridManager.GetPositionFromCoordinates(_pathfinder.StartCoordinates);
        }

        private void FinishPath()
        {
            _enemy.StealGold();
            gameObject.SetActive(false);
        }

        private IEnumerator FollowPath() 
        {
            foreach (Node node in _path)
            {
                Vector3 startPosition = transform.position;
                Vector3 endPosition = _gridManager.GetPositionFromCoordinates(node.Coordinates);
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