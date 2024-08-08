using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRadius : MonoBehaviour
{
    [SerializeField] Enemy parent;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != parent && other.GetComponent<Unit>())
            parent.SetTarget(other.GetComponent<Unit>());
    }
}