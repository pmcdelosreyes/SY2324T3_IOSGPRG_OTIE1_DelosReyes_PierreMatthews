using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    int dmg = 1;
    public float speed;
    bool isAlive = true;

    [SerializeField] TextMeshProUGUI deathConText;

    Player.swipe deathCon;
    int randCond;

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        randCond = Random.Range(0, 3);
        setDeathCondition(randCond); // set a death condition to enemy
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("swipe: " + deathCon + " | input:" + other.GetComponent<Player>().input); // check kill condition & current player input

        if (other.GetComponent<Player>().input == this.deathCon)
        {
            this.isAlive = false;
            Destroy(gameObject); // destroy enemy upon contact with player
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.GetComponent("Player") && this.isAlive == true) // check if enemy is detected by player
        {
            // deal damage to player
            other.GetComponent<Player>().hp -= dmg;
            Destroy(gameObject); // destroy enemy upon contact with player
        }

        // kill player when out of hp
        if (other.GetComponent<Player>().hp < 1)
        {
            Destroy(other.gameObject); // destroy player when life is 0
        }
    }

    private void setDeathCondition(int randCond)
    {
        // [RED ARROW ENEMY]
        if (this.CompareTag("Red Arrow"))
        {
            if (randCond == 0)
            {
                deathCon = Player.swipe.Right;
                UpdateText("Swipe Left!");
            }
            else if (randCond == 1)
            {
                deathCon = Player.swipe.Left;
                UpdateText("Swipe Right!");
            }
            else if (randCond == 2)
            {
                deathCon = Player.swipe.Down;
                UpdateText("Swipe Up!");
            }
            else if (randCond == 3)
            {
                deathCon = Player.swipe.Up;
                UpdateText("Swipe Down!");
            }
        }
        // [GREEN ARROW ENEMY]
        else if (this.CompareTag("Green Arrow"))
        {
            if (randCond == 0)
            {
                deathCon = Player.swipe.Left;
                UpdateText("Swipe Left!");
            }
            else if (randCond == 1)
            {
                deathCon = Player.swipe.Right;
                UpdateText("Swipe Right!");
            }
            else if (randCond == 2)
            {
                deathCon = Player.swipe.Up;
                UpdateText("Swipe Up!");
            }
            else if (randCond == 3)
            {
                deathCon = Player.swipe.Down;
                UpdateText("Swipe Down!");
            }
        }
    }

    void UpdateText(string msg)
    {
        deathConText.text = msg;
    }
}
