using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotate : Arrow
{
    int currrentRotation = 0;
    new SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();


        InvokeRepeating(nameof(Rotate), 0, 0.2f);

        chosenSpriteIndex = Random.Range(0, 4);

        switch (chosenSpriteIndex)
        {
            case 0:
                killDirection = DirectionType.Down;
                break;
            case 1:
                killDirection = DirectionType.Left;
                break;
            case 2:
                killDirection = DirectionType.Right;
                break;
            case 3:
                killDirection = DirectionType.Up;
                break;
            default:
                break;
        }
    }

    private void Rotate()
    {
        currrentRotation++;
        currrentRotation %= 4;

        renderer.sprite = arrowSprites[currrentRotation];

    }

    public override void Reveal()
    {
        CancelInvoke(nameof(Rotate));

        renderer.sprite = arrowSprites[chosenSpriteIndex];
        renderer.color = Color.yellow;
    }
}
