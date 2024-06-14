using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRed : Arrow
{
    private void Awake()
    {
        chosenSpriteIndex = Random.Range(0, 4);

        switch (chosenSpriteIndex)
        {
            case 0:
                killDirection = DirectionType.Up;
                break;
            case 1:
                killDirection = DirectionType.Right;
                break;
            case 2:
                killDirection = DirectionType.Left;
                break;
            case 3:
                killDirection = DirectionType.Down;
                break;
            default:
                break;
        }
    }

    public override void Reveal()
    {
        var renderer = GetComponent<SpriteRenderer>();

        renderer.sprite = arrowSprites[chosenSpriteIndex];

        renderer.color = Color.red;
    }
}
