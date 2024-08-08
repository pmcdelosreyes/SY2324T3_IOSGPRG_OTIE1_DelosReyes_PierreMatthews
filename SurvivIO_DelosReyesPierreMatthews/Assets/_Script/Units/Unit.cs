using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Unit Stats
    public Health hp;
    public float moveSpeed;
    // Unit Weapons & Ammo
    public Gun curGun;
    public Gun pGun;
    public Gun sGun;
    public Ammo ammo;

    public void InitUnit(Health nHp, float nMoveSpeed, Ammo nAmmo)
    {
        hp = nHp;
        moveSpeed = nMoveSpeed;
        ammo = nAmmo;
    }

    public virtual void UnitShoot()
    {
        curGun.Shoot();
        Debug.Log("Unit Shot");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("unit hit");
        // take damage
        if (other.gameObject.GetComponent<Bullet>())
        {
            Debug.Log(other.gameObject.name + "took damage");
            GetComponent<Health>().TakeDmg(this.curGun.bulletPrefab.GetComponent<Bullet>().bulletDmg);
        }
    }
}