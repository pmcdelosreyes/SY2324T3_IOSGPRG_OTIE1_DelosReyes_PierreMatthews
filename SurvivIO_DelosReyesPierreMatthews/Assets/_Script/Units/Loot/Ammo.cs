using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int pAmmo;
    public int sAmmo;
    public int aAmmo;
    GunType gunType;

    public enum GunType
    {
        Pistol,
        Shotgun,
        ARifle,
    }

    public void InitAmmo(int nPAmmo, int nSAmmo, int nAAmmo)
    {
        pAmmo = nPAmmo;
        sAmmo = nSAmmo;
        aAmmo = nAAmmo;
    }

    private void Start()
    {
        // set ammo type
        int randType = Random.Range(0, 3);
        if (!this.GetComponent<Player>() && !this.GetComponent<Enemy>())
        {
            switch (randType)
            {
                case 0:
                    this.gunType = GunType.Pistol;
                    this.GetComponent<SpriteRenderer>().color = Color.white;
                    break;

                case 1:
                    this.gunType = GunType.Shotgun;
                    this.GetComponent<SpriteRenderer>().color = Color.gray;
                    break;

                case 2:
                    this.gunType = GunType.ARifle;
                    this.GetComponent<SpriteRenderer>().color = Color.black;
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            // add random amount of ammo to player
            int randAmount = Random.Range(5, 10);
            switch (gunType)
            {
                case GunType.Pistol:
                    GameManager.Instance.Player.playerAmmo.pAmmo += randAmount;
                    break;
                case GunType.Shotgun:
                    GameManager.Instance.Player.playerAmmo.sAmmo += randAmount;
                    break;
                case GunType.ARifle:
                    GameManager.Instance.Player.playerAmmo.aAmmo += randAmount;
                    break;
            }
        }
    }
}