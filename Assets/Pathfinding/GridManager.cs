using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Vector2Int _gridSize;
        [Tooltip("World Grid Size - Should match the UnityEditor snap settings")]
        [SerializeField] private int _unityGridSize = 10;
        public int UnityGridSize => _unityGridSize;

        private Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();
        public Dictionary<Vector2Int, Node> Grid => _grid;

        private void Awake()
        {
            CreateGrid();
        }
        
        public Node GetNode(Vector2Int coordinates)
        {
            if (_grid.ContainsKey(coordinates))
            {
                return _grid[coordinates];
            }

            return null;
        }

        public void BlockNode(Vector2Int coordinates)
        {
            if (_grid.ContainsKey(coordinates))
            {
                _grid[coordinates].IsWalkable = false;
            }
        }

        public void ResetNodes()
        {
            foreach (KeyValuePair<Vector2Int,Node> entry in _grid)
            {
                entry.Value.ConnectedTo = null;
                entry.Value.IsExplored = false;
                entry.Value.IsPath = false;
            }
        }

        public Vector2Int GetCoordinatesFromPosition(Vector3 position)
        {
            var coordinates = new Vector2Int
            {
                x = Mathf.RoundToInt(position.x / _unityGridSize),
                y = Mathf.RoundToInt(position.z / _unityGridSize)
            };

            return coordinates;
        }

        public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
        {
            var position = new Vector3
            {
                x = coordinates.x * _unityGridSize,
                z = coordinates.y * _unityGridSize
            };

            return position;
        }

        private void CreateGrid()
        {
            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = 0; y < _gridSize.y; y++)
                {
                    var coordinates = new Vector2Int(x, y);
                    _grid.Add(coordinates, new Node(coordinates, true));
                }
            }
        } 
    }
}
