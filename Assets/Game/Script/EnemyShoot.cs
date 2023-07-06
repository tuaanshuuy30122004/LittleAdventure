using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject DamageOrbPrefabs;
    private Character cc;
    private void Awake()
    {
        cc = GetComponent<Character>();
    }

    public void Shoot()
    {
        Instantiate(DamageOrbPrefabs, shootingPoint.position, Quaternion.LookRotation(shootingPoint.forward));
    }

    private void Update()
    {
        if (cc != null)
        {
            cc.RotateToTarget();
        }
    }
}
