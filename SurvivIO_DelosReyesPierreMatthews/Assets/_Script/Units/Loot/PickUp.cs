using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    Inventory inventory;
    public GameObject gunUI;
    private void Start()
    {
        if (GameManager.Instance.Player == null) Debug.Log("yo");
        inventory = GameManager.Instance.Player.GetComponent<Inventory>(); // initialize player inventory
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() && GetComponent<Gun>()) // check if player collided with a gun
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                // pick up item
                if (inventory.isFull[i] == false)
                {
                    Instantiate(gunUI, inventory.slots[i].transform, false); // display picked up gun ui
                    inventory.isFull[i] = true;
                    break;
                }
                else if (inventory.isFull[i] == true)
                {
                    Destroy(inventory.slots[i].GetComponentInChildren<Gun>()); // destory previous gun UI
                    inventory.isFull[i] = false;
                }
            }
        }

        if (other.GetComponent<Player>())
            Destroy(gameObject); // destroy lootable item
        else
            return;
    }
}