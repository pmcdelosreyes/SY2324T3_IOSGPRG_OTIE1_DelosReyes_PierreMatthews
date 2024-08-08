using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDmg;

    private void Start()
    {
        bulletDmg = Random.Range(1f, 2f);
    }

    private void Update()
    {
        float bulletLimit = 10f;
        if (this.transform.position.x > bulletLimit || this.transform.position.x < -bulletLimit || this.transform.position.y > bulletLimit || this.transform.position.y < -bulletLimit)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BoxCollider2D>())
        {
            Debug.Log("hit: " + other.gameObject.name);
            Destroy(gameObject); // destroys bullet upon contact

            other.GetComponent<Health>().TakeDmg(bulletDmg); // deal damage to hit unit
        }
        else
            return;
    }
}
