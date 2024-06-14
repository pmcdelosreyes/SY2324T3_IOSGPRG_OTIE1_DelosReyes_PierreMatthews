using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arrow : MonoBehaviour
{
    public DirectionType killDirection;
    [SerializeField] protected Sprite[] arrowSprites;

    protected int chosenSpriteIndex;

    public abstract void Reveal();
}