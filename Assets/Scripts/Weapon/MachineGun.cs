using System;
using System.Collections;
using UnityEngine;

public class MachineGun : Weapon
{
    [SerializeField] private int _bulletsPerSecond = 3;

    private bool _isShooting;

    public override void Shoot(Transform shootPoint)
    {
        if (_isShooting == false)
            StartCoroutine(SpawnBullets(shootPoint));
    }

    private void OnValidate()
    {
        if (_bulletsPerSecond <= 1)
            throw new ArgumentOutOfRangeException("Bullets per second", "Must be greater then 1");
    }

    private IEnumerator SpawnBullets(Transform shootPoint)
    {
        float timeBetweenShoots =  1 / (float)_bulletsPerSecond;
        WaitForSeconds delay = new WaitForSeconds(timeBetweenShoots);

        _isShooting = true;

        for (int i = 0; i < _bulletsPerSecond; i++)
        {
            Instantiate(Bullet, shootPoint.position, Quaternion.identity);
            yield return delay;
        }

        _isShooting = false;
    }
}
