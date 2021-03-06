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
            ReturnToStart();
            RecalculatePath(true);
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

        private void RecalculatePath(bool resetPath)
        {
            Vector2Int coordinates;

            if (resetPath)
            {
                coordinates = _pathfinder.StartCoordinates;
            }
            else
            {
                coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);
            }
            
            StopAllCoroutines();
            
            _path.Clear();
            _path = _pathfinder.GetNewPath(coordinates);
            
            StartCoroutine(FollowPath());
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
            for (int i = 1; i < _path.Count; i++)
            {
                Vector3 startPosition = transform.position;
                Vector3 endPosition = _gridManager.GetPositionFromCoordinates(_path[i].Coordinates);
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