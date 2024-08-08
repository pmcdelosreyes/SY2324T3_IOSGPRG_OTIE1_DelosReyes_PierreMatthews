using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARifle : Gun
{
    void Start()
    {
        InitGun(0.1f, 10f, 1.5f, 30); // initialize pistol stats
        readyToShoot = true;
    }

    public override void Shoot()
    {
        if (curClip > 0 && readyToShoot)
        {
            readyToShoot = false; // disable shooting prep when already shooting
            // Shoot Mechanic
            base.Shoot();
            curClip--; // decrease ammo in clip
            Invoke(nameof(ReadyShot), fireRate); // enable shooting prep after interval
        }
        else if (curClip <= 0)
            Reload(); 
    }
    public override void ReloadFinish()
    {
        int rem = 0;
        //Debug.Log(GameManager.Instance.Player.playerAmmo.aAmmo);
        if (unitHolder.ammo.aAmmo >= maxClip)
        {
            Debug.Log("excess ammo reload");
            curClip += unitHolder.ammo.aAmmo;
            rem = curClip - maxClip;
            curClip -= rem;
            unitHolder.ammo.aAmmo = rem;
        }
        else if (unitHolder.ammo.aAmmo + curClip > maxClip)
        {
            curClip += unitHolder.ammo.aAmmo;
            rem = curClip - maxClip;
            curClip -= rem;
            unitHolder.ammo.aAmmo = rem;
            Debug.Log(unitHolder.ammo.aAmmo);
        }
        else if (unitHolder.ammo.aAmmo + curClip < maxClip)
        {
            curClip += unitHolder.ammo.aAmmo;
            unitHolder.ammo.aAmmo = 0;
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
                GameManager.Instance.Player.pGun = GameManager.Instance.Player.GetComponentInChildren<ARifle>();
            }
            else if (GameManager.Instance.Player.sGun == null)
            {
                GameManager.Instance.Player.sGun = GameManager.Instance.Player.GetComponentInChildren<ARifle>();
            }
            else
            {
                GameManager.Instance.Player.pGun = null; // remove previous gun
                GameManager.Instance.Player.pGun = GameManager.Instance.Player.GetComponentInChildren<ARifle>();
            }
            // activate gun on player
            GameManager.Instance.Player.curGun = GameManager.Instance.Player.GetComponentInChildren<ARifle>();
            GameManager.Instance.Player.GetComponentInChildren<Pistol>().GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Player.GetComponentInChildren<Shotgun>().GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Player.GetComponentInChildren<ARifle>().GetComponent<SpriteRenderer>().enabled = true;
            Destroy(gameObject);// destroy
        }
    }
}