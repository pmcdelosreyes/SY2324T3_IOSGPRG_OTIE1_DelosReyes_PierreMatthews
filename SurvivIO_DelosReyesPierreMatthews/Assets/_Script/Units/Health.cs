using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float curHp;
    public float maxHp;

    public UnityEvent OnDeathEvent = new UnityEvent();

    public void InitHealth(float nMaxHp)
    {
        this.maxHp = nMaxHp;
        this.curHp = nMaxHp;
    }

    public void TakeDmg(float damage)
    {
        if (curHp > 1)
            curHp -= damage;
        else if (curHp < 1)
        {
            curHp = 0;
            if (OnDeathEvent != null && GetComponent<Player>())
            {
                OnDeathEvent.Invoke();
                Death();
            }
        }
    }

    public void Death()
    {
        Debug.Log(gameObject.name + " Died");
        Destroy(this.gameObject); // destroy game object
    }
}