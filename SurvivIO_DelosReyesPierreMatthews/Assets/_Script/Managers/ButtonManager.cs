using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void ShootCurGun()
    {
        Player player = GameManager.Instance.Player;
        Debug.Log("currently shooting");
        if (player.curGun != null && player.curGun.readyToShoot && !player.curGun.isShooting && !player.curGun.reloading && player.curGun.curClip > 0)
        {
            player.curGun.Shoot();
        }
        else
            Debug.Log("Equipped Gun Error - Shooting");
    }

    public void ReloadCurGun()
    {
        Player player = GameManager.Instance.Player;

        if (player.curGun != null && !player.curGun.isShooting && !player.curGun.reloading && player.curGun.curClip < player.curGun.maxClip)
            Invoke(nameof(ReloadHolder), player.curGun.reloadTime);
        else
            Debug.Log("Equipped Gun Error - Reloading");
    }

    public void ReloadHolder()
    {
        GameManager.Instance.Player.curGun.Reload();
    }

    public void SwitchToPrimary()
    {
        GameManager.Instance.Player.curGun = GameManager.Instance.Player.pGun; // change current gun to primary gun
        Debug.Log("switched to primary");

        if (GameManager.Instance.Player.curGun == GameManager.Instance.Player.curGun.GetComponent<Pistol>())
        {
            GameManager.Instance.Player.GetComponentInChildren<Shotgun>().GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Player.GetComponentInChildren<ARifle>().GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Player.GetComponentInChildren<Pistol>().GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("switched to pistol");
        } 
        else if (GameManager.Instance.Player.curGun == GameManager.Instance.Player.curGun.GetComponent<Shotgun>())
        {
            GameManager.Instance.Player.GetComponentInChildren<Pistol>().GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Player.GetComponentInChildren<ARifle>().GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Player.GetComponentInChildren<Shotgun>().GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("switched to shotgun");
        }
        else if (GameManager.Instance.Player.curGun == GameManager.Instance.Player.curGun.GetComponent<ARifle>())
        {
            GameManager.Instance.Player.GetComponentInChildren<Pistol>().GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Player.GetComponentInChildren<Shotgun>().GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Player.GetComponentInChildren<ARifle>().GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("switched to rifle");
        }
    }

    public void SwitchToSecondaryy()
    {
        GameManager.Instance.Player.curGun = GameManager.Instance.Player.sGun; // change current gun to secondary gun
        Debug.Log("switched to secondary");

        if (GameManager.Instance.Player.curGun == GameManager.Instance.Player.curGun.GetComponent<Pistol>())
        {
            GameManager.Instance.Player.GetComponentInChildren<Shotgun>().GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Player.GetComponentInChildren<ARifle>().GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Player.GetComponentInChildren<Pistol>().GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("switched to pistol");
        }
        else if (GameManager.Instance.Player.curGun == GameManager.Instance.Player.curGun.GetComponent<Shotgun>())
        {
            GameManager.Instance.Player.GetComponentInChildren<Pistol>().GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Player.GetComponentInChildren<ARifle>().GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Player.GetComponentInChildren<Shotgun>().GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("switched to shotgun");
        }
        else if (GameManager.Instance.Player.curGun == GameManager.Instance.Player.curGun.GetComponent<ARifle>())
        {
            GameManager.Instance.Player.GetComponentInChildren<Pistol>().GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Player.GetComponentInChildren<Shotgun>().GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.Player.GetComponentInChildren<ARifle>().GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("switched to rifle");
        }
    }
}