using System;
using System.Collections;
using UnityEngine;

namespace Tower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private int _cost = 75;
        [SerializeField] private float _buildDelay = 1f;

        private void Start()
        {
            StartCoroutine(Build());
        }

        private IEnumerator Build()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);

                foreach (Transform grandchild in child)
                {
                    grandchild.gameObject.SetActive(false);
                }
            }
            
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);

                yield return new WaitForSeconds(_buildDelay);

                foreach (Transform grandchild in child)
                {
                    grandchild.gameObject.SetActive(true);
                }
            }
        }

        public bool CreateTower(Tower tower, Vector3 position)
        {
            var bank = FindObjectOfType<Bank>();

            if (bank == null)
            {
                return false;
            }

            if (bank.CurrentBalance >= _cost)
            {
                Instantiate(tower.gameObject, position, Quaternion.identity);
                bank.Withdraw(_cost);
                return true;
            }

            return false;
        }
    }
}
