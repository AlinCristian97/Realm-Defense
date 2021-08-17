using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Pathfinder : MonoBehaviour
    {
        [SerializeField] private Vector2Int _startCoordinates;
        [SerializeField] private Vector2Int _endCoordinates;
        public Vector2Int StartCoordinates => _startCoordinates;
        public Vector2Int EndCoordinates => _endCoordinates;

        
        private Node _startNode;
        private Node _endNode;
        private Node _currentSearchNode;

        private Queue<Node> _frontier = new Queue<Node>();
        private Dictionary<Vector2Int, Node> _reached = new Dictionary<Vector2Int, Node>();

        private Vector2Int[] _directions =
        {
            Vector2Int.right,
            Vector2Int.left,
            Vector2Int.up,
            Vector2Int.down
        };
        private GridManager _gridManager;
        private Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();


        private void Awake()
        {
            _gridManager = FindObjectOfType<GridManager>();

            if (_gridManager != null)
            {
                _grid = _gridManager.Grid;
                
                _startNode = _grid[_startCoordinates];
                _endNode = _grid[_endCoordinates];
            }
        }

        private void Start()
        {
            GetNewPath();
        }

        public List<Node> GetNewPath()
        {
            _gridManager.ResetNodes();
            BreadthFirstSearch();
            return BuildPath();
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
                }
            }

            foreach (Node neighbour in neighbours)
            {
                if (!_reached.ContainsKey(neighbour.Coordinates) && neighbour.IsWalkable)
                {
                    neighbour.ConnectedTo = _currentSearchNode;
                    
                    _reached.Add(neighbour.Coordinates, neighbour);
                    _frontier.Enqueue(neighbour);
                }
            }
        }

        private void BreadthFirstSearch()
        {
            _startNode.IsWalkable = true;
            _endNode.IsWalkable = true;
            
            _frontier.Clear();
            _reached.Clear();
            
            bool isRunning = true;
            
            _frontier.Enqueue(_startNode);
            _reached.Add(_startCoordinates, _startNode);

            while (_frontier.Count > 0 && isRunning)
            {
                _currentSearchNode = _frontier.Dequeue();
                _currentSearchNode.IsExplored = true;
                ExploreNeighbours();

                if (_currentSearchNode.Coordinates == _endCoordinates)
                {
                    isRunning = false;
                }
            }
        }

        private List<Node> BuildPath()
        {
            List<Node> path = new List<Node>();
            Node currentNode = _endNode;
            
            path.Add(currentNode);
            currentNode.IsPath = true;
            
            //while currentNode.ConnectedTo != null
                // set currentNode to currentNode.connectedTo
                // add currentNode to path

            while (currentNode.ConnectedTo != null)
            {
                currentNode = currentNode.ConnectedTo;
                path.Add(currentNode);
                currentNode.IsPath = true;
            }
            
            path.Reverse();

            return path;
        }

        public bool WillBlockPath(Vector2Int coordinates)
        {
            if (_grid.ContainsKey(coordinates))
            {
                bool previousState = _grid[coordinates].IsWalkable;
                
                _grid[coordinates].IsWalkable = false;
                List<Node> newPath = GetNewPath();
                _grid[coordinates].IsWalkable = previousState;

                if (newPath.Count <= 1)
                {
                    GetNewPath();
                    return true;
                }
            }

            return false;
        }
    }
}
