using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public int bulletAmount;
    public float bulletSpread;

    void Start()
    {
        InitGun(2f, 8f, 2f, 5); // initialize pistol stats
        readyToShoot = true;
    }

    public override void Shoot()
    {
        if (curClip > 0 && readyToShoot)
        {
            readyToShoot = false; // disable shooting prep when already shooting

            // Shoot Mechanic
            for (int i = 0; i < bulletAmount; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // create bullet
                Rigidbody2D bulletRb2d = bullet.GetComponent<Rigidbody2D>();
                Vector2 dir = transform.rotation * Vector2.up;
                Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-bulletSpread, bulletSpread);
                bulletRb2d.velocity = (dir + pdir) * bulletForce;
            }

            curClip--; // decrease ammo in clip
            Invoke(nameof(ReadyShot), fireRate); // enable shooting prep after interval
        }
        else if (curClip <= 0)
            Reload();
    }

    public override void ReloadFinish()
    {
        int rem = 0;
        //Debug.Log(GameManager.Instance.Player.playerAmmo.sAmmo);
        if (unitHolder.ammo.sAmmo >= maxClip) // shotgun ammo > max clip
        {
            Debug.Log("excess ammo reload");
            curClip += unitHolder.ammo.sAmmo;
            rem = curClip - maxClip;
            curClip -= rem;
            unitHolder.ammo.sAmmo = rem;
        }
        else if (unitHolder.ammo.sAmmo + curClip > maxClip)
        {
            curClip += unitHolder.ammo.sAmmo;
            rem = curClip - maxClip;
            curClip -= rem;
            unitHolder.ammo.sAmmo = rem;
            Debug.Log(unitHolder.ammo.sAmmo);
        }
        else if (unitHolder.ammo.sAmmo + curClip < maxClip)
        {
            curClip += unitHolder.ammo.sAmmo;
            unitHolder.ammo.sAmmo = 0;
        }
        base.ReloadFinish();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            // set gun as weapon in player inventory
            if (GameManager.Instance.Player.pGun == null)
            {
                GameManager.Instance.Player.pGun = GameManager.Instance.Player.GetComponentInChildren<Shotgun>();
            }
            else if (GameManager.Instance.Player.sGun == null)
            {
                GameManager.Instance.Player.sGun = GameManager.Instance.Player.GetComponentInChildren<Shotgun>();
            }
            else
            {
                GameManager.Instance.Player.pGun = null; // remove previous gun
                GameManager.Instance.Player.pGun = GameManager.Instance.Player.GetComponentInChildren<Shotgun>();
            }

            // activate gun on player (disable unused gun)
            GameManager.Instance.Player.curGun = GameManager.Instance.Player.GetComponentInChildren<Shotgun>();
            GameManager.Instance.Player.GetComponentInChildren<Pistol>().GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Player.GetComponentInChildren<ARifle>().GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Player.GetComponentInChildren<Shotgun>().GetComponent<SpriteRenderer>().enabled = true;

            Destroy(gameObject);// destroy
        }
    }
}
