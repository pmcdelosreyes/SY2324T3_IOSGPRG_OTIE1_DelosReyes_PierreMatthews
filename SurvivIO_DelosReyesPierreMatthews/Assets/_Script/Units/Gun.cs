using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Gun stats
    //public float damage;
    public float fireRate, range, reloadTime;
    public int curClip, maxClip, shotClip; // current bullets in clip | maximum bulllets in clip | number of bullets shot
    public bool allowHoldBtn;
    public float bulletForce = 1f;

    // Bool checkers
    public bool isShooting, readyToShoot, reloading;

    // Reference
    public Camera cam;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Unit unitHolder;

    public void InitGun(float nFireRate, float nRange, float nReloadTime, int nMaxClip)
    {
        //damage = nDamage;
        //this.bulletPrefab.GetComponent<Bullet>().SetBulletDmg(nDamage);
        fireRate = nFireRate;
        range = nRange;
        reloadTime = nReloadTime;
        curClip = nMaxClip;
        maxClip = nMaxClip;
    }

    public virtual void Shoot()
    {
        //Debug.Log("bang");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // create bullet
        Rigidbody2D bulletRb2d = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = transform.rotation * Vector2.up;
        bulletRb2d.velocity = dir  * bulletForce;// launch bullet
    }

    public void ReadyShot()
    {
        readyToShoot = true;
    }

    public void Reload()
    {
        if (unitHolder.ammo.pAmmo > 0 || unitHolder.ammo.sAmmo > 0 || unitHolder.ammo.aAmmo > 0)
        {
            reloading = true;
            Invoke(nameof(ReloadFinish), reloadTime);
        }
        else
            Debug.Log("Not enough ammo in inventory");
    }

    public virtual void ReloadFinish()
    {
        //curClip = maxClip;
        reloading = false;
    }
}
