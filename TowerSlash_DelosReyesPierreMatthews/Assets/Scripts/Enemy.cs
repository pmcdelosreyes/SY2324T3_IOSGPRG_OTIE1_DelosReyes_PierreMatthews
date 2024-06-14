using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed { get { return speed; } set { speed = value; } }

    [SerializeField] float speed = 1;

    // TODO: Remove both
    [SerializeField] Sprite[] arrowSprite;
    [SerializeField] GameObject arrowDirection;

    public Arrow currentArrow;

    void Update()
    {

        transform.Translate(Vector2.down * speed * Time.deltaTime); // falling enemy
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent("InputHandler"))
        {
            GameManager.Instance.AddEnemyToList(this);
            currentArrow.Reveal();
        }
        else if (other.GetComponent<Player>())
        {
            GameManager.Instance.Player.TakeDmg(); // deduct player hp
            KillSelf();
        }
        else
            Debug.Log("BUG!");
        
    }

    public void KillSelf()
    {
        //remove enemy
        //Debug.Log(currentArrow.killDirection + " killed type: " + currentArrow.name);

        GameManager.Instance.RemoveEnemyFromList(this);
        Destroy(gameObject);
    }

}
