using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject peasant;
    [SerializeField] GameObject tank;
    [SerializeField] GameObject assassin;
    [SerializeField] Vector2 characterPosition;
    [SerializeField] Vector2 offScreen;
    [SerializeField] int characterInt = 1;
    [SerializeField] SpriteRenderer peasantRenderer, tankRenderer, assassinRenderer;
    [SerializeField] TextMeshProUGUI typeTxt;

    public static string chosenCharacter;

    private void Awake()
    {
        characterPosition = peasant.transform.position;
        offScreen = tank.transform.position;
        peasantRenderer = peasant.GetComponent<SpriteRenderer>();
        tankRenderer = peasant.GetComponent<SpriteRenderer>();
        assassinRenderer = peasant.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (characterInt == 1)
        {
            chosenCharacter = "Human";
        }
        else if (characterInt == 2)
        {
            chosenCharacter = "Tank";
        }
        else if (characterInt == 3)
        {
            chosenCharacter = "Rogue";
        }


        typeTxt.text = chosenCharacter;

    }

    public void GoToScene(int _scene)
    {
        SceneManager.LoadScene(_scene);
        GameManager.Instance.StartSpawning();
    }

    public void NextCharacter()
    {
        Debug.Log("clicked");
        switch(characterInt)
        {
            case 1:
                Debug.Log("next");
                peasantRenderer.enabled = false;
                peasant.transform.position = offScreen;

                tank.transform.position = characterPosition;
                tankRenderer.enabled = true;

                characterInt++;
                break;

            case 2:
                Debug.Log("next");
                tankRenderer.enabled = false;
                tank.transform.position = offScreen;

                assassin.transform.position = characterPosition;
                assassinRenderer.enabled = true;

                characterInt++;
                break;

            case 3:
                Debug.Log("next");
                assassinRenderer.enabled = false;
                assassin.transform.position = offScreen;

                peasant.transform.position = characterPosition;
                peasantRenderer.enabled = true;

                characterInt++;
                ResetInt();
                break;

            default:
                ResetInt();
                break;

        }
    }
    
    public void PreviousCharacter()
    {
        switch (characterInt)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;

            default:
                break;

        }
    }

    private void ResetInt()
    {
        if (characterInt >= 3)
        {
            characterInt = 1;
        }
    }
}
