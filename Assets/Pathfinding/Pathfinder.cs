using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Pathfinder : MonoBehaviour
    {
        [SerializeField] private Node _currentSearchNode;

        private Vector2Int[] _directions =
        {
            Vector2Int.right,
            Vector2Int.left,
            Vector2Int.up,
            Vector2Int.down
        };

        private GridManager _gridManager;
        private Dictionary<Vector2Int, Node> _grid;

        private void Awake()
        {
            _gridManager = FindObjectOfType<GridManager>();

            if (_gridManager != null)
            {
                _grid = _gridManager.Grid;
            }
        }

        private void Start()
        {
            ExploreNeighbours();
        }

        private void ExploreNeighbours()
        {
            // create empty list "neighbours"
            // loop through each of the 4 directions in our directions array 
            // calculate the coordinates of the node in that direction from our currentSearchNode
            // check if the neighbour's coordinates exist in the grid
            // if it exists, add it to our neighbours list

            List<Node> neighbours = new List<Node>();

            foreach (Vector2Int direction in _directions)
            {
                Vector2Int neighbourCoordinates = _currentSearchNode.Coordinates + direction;

                if (_grid.ContainsKey(neighbourCoordinates))
                {
                    neighbours.Add(_grid[neighbourCoordinates]);
                    
                    //TODO: Remove after testing
                    _grid[neighbourCoordinates].IsExplored = true;
                    _grid[_currentSearchNode.Coordinates].IsPath = true;
                }
            }
        }
    }
}
