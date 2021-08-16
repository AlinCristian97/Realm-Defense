using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int _maxHitPoints = 5;
        private int _currentHitPoints = 0;

        private global::Enemy.Enemy _enemy;

        private void OnEnable()
        {
            _currentHitPoints = _maxHitPoints;
        }

        private void Start()
        {
            _enemy = GetComponent<global::Enemy.Enemy>();
        }

        private void OnParticleCollision(GameObject other)
        {
            ProcessHit();
        }

        private void ProcessHit()
        {
            _currentHitPoints--;

            if (_currentHitPoints <= 0)
            {
                gameObject.SetActive(false);
                _enemy.RewardGold();
            }
        }
    }
}