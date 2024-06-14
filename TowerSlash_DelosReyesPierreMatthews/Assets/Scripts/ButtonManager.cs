using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    public float FillTotal { get { return fillTotal; } set { fillTotal = value; } }
    public Image OverlayCoolDown { get { return overlayCooldown; } set { overlayCooldown = value; } }

    public Image overlayCooldown;
    public float fillTotal = 1; // 1 = on cooldown & 0 = ready

    [SerializeField] Button dashBtn;


    // Handles all button related stuff

    void Start()
    {
        GameManager.Instance.ButtonManager = this;
    }

    void Update()
    {
        if (GameManager.Instance.Player.DashGauge < 0.01)
        {
            // activate button
            dashBtn.interactable = true;
        }
    }

    public void DashSkill()
    {
        StartCoroutine(ActivateDash());
        GameManager.Instance.Player.DashGauge = 1;
        dashBtn.interactable = false;
    }

    IEnumerator ActivateDash()
    {
        GameManager.Instance.Player.IsDashing = true;

        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.transform.Translate(Vector2.down * 1000 * Time.deltaTime);
        }
        
        yield return new WaitForSeconds(5);

        // return to normal
        GameManager.Instance.Player.IsDashing = false;
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.transform.Translate(Vector2.down * 1 * Time.deltaTime);
        }
    }

    
}
