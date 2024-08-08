using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpTxt;
    [SerializeField] TextMeshProUGUI clipTxt;
    [SerializeField] TextMeshProUGUI gunTxt;
    void Update()
    {
        hpTxt.text = "HP: " + (int)GameManager.Instance.Player.hp.curHp;
        if (GameManager.Instance.Player.curGun != null)
        {
            if (GameManager.Instance.Player.curGun.GetComponent<Pistol>())
                gunTxt.text = "Pistol";
            else if (GameManager.Instance.Player.curGun.GetComponent<Shotgun>())
                gunTxt.text = "Shotgun";
            else if (GameManager.Instance.Player.curGun.GetComponent<ARifle>())
                gunTxt.text = "Auto-Rifle";

            clipTxt.text = GameManager.Instance.Player.curGun.curClip + " / " + GameManager.Instance.Player.curGun.maxClip;
        }
        else
        {
            gunTxt.text = "No Gun";
            clipTxt.text = "";
        }
    }
}