using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public bool IsDashing { get { return isDashing; } set { isDashing = value; } }
    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
    public float DashGauge { get { return dashGauge; } set { dashGauge = value; } }
    public string PlayerType { get { return playerType; } set { playerType = value; } }

    [SerializeField] float dashGauge = 1;
    [SerializeField] float gaugeIncrease;
    [SerializeField] bool isDashing = false;
    [SerializeField] bool isAlive = true;
    [SerializeField] int hp;
    [SerializeField] string playerType;

    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] TextMeshProUGUI hpTxt;

    void Start()
    {
        GameManager.Instance.Player = this;

        string playerType = Menu.chosenCharacter;
        //Debug.Log("Player Type:" + playerType);
        if (playerType == "Tank")
        {
            Debug.Log("Tank");
            hp = 7;
            gaugeIncrease = 0.05f;
            GetComponent<SpriteRenderer>().color = new Color(.53f, .39f, .7f, 1.0f);
        }
        else if (playerType == "Rogue")
        {
            Debug.Log("Rogue");
            hp = 4;
            gaugeIncrease = 0.1f;
            GetComponent<SpriteRenderer>().color = new Color(.17f, .05f, .33f, 1.0f);
        }        
        else if (playerType == "Human")
        {
            Debug.Log("Human");
            hp = 5;
            gaugeIncrease = 0.05f;
            GetComponent<SpriteRenderer>().color = new Color(.73f, .59f, .88f, 1.0f);
        }
    }

   

    void Update()
    {
        hpTxt.text = "HP: " + hp;
        scoreTxt.text = "Score: " + (int)GameManager.Instance.Score;        
    }

    public void TakeDmg()
    {
        if (isDashing == false)
        {
            if (hp > 1)
                hp -= 1;
            else
                hp = 0;
        }

        if (hp < 1)
        {
            GameManager.Instance.Die();
        }
    }

    public void AddHp()
    {
        Debug.Log("Gained HP");
        if (hp < 4)
            hp = 3;
        else
            hp += 1;
    }

    public void IncreaseGauge()
    {
        dashGauge -= gaugeIncrease;
        GameManager.Instance.ButtonManager.OverlayCoolDown.GetComponent<Image>().fillAmount = dashGauge;
    }

    
}
