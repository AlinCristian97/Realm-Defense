using UnityEngine;

namespace Pathfinding
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Node _node;

        private void Start()
        {
            Debug.Log(_node.Coordinates);
            Debug.Log(_node.IsWalkable);
        }

        private void Update()
        {
        
        }
    }
}
