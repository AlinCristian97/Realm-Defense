using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform _weapon;
    [SerializeField] private ParticleSystem _projectileParticles;
    [SerializeField] private float _range = 15f;
    private Transform _target;

    private void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        _target = closestTarget;
    }

    private void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, _target.position);
        
        _weapon.LookAt(_target);

        Attack(targetDistance < _range);
    }

    private void Attack(bool isActive)
    {
        ParticleSystem.EmissionModule emissionModule = _projectileParticles.emission;
        emissionModule.enabled = isActive;
    }
}