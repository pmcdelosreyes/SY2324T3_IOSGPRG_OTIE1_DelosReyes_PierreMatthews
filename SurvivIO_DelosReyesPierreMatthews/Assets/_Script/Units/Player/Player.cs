using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public Health playerHealth;
    Rigidbody2D rb2dPlayer;
    public Ammo playerAmmo;
    // Player Joysticks
    [SerializeField] Joystick movementJoystick;
    [SerializeField] Joystick lookJoystick;

    private void Awake()
    {
        // Initialize as player in game manager
        GameManager.Instance.Player = this;
        // Initialize local variables
        playerHealth = GetComponent<Health>();
        rb2dPlayer = GetComponent<Rigidbody2D>();
        playerAmmo = GetComponent<Ammo>();
    }

    private void Start()
    {
        // initialize player stats
        playerHealth.InitHealth(100);
        InitUnit(playerHealth, 4f, playerAmmo);
        playerHealth.OnDeathEvent.AddListener(PlayerDied); // registers function as listener
        playerHealth.OnDeathEvent.AddListener(GameManager.Instance.GameOver); // registers function as listener
    }

    void FixedUpdate()
    {
        // for [ MOBILE CONTROLS ]
        if (movementJoystick.joystickVec.y != 0) // check if joystick vector has moved
        {
            rb2dPlayer.velocity = new Vector2(movementJoystick.joystickVec.x * this.moveSpeed,
               movementJoystick.joystickVec.y * this.moveSpeed);
        }
        else // if not moving
        {
            rb2dPlayer.velocity = Vector2.zero;
        }
        // Player Rotation/Aiming
        Vector3 moveVector = (Vector3.up * lookJoystick.joystickVec.y - Vector3.left * lookJoystick.joystickVec.x);
        if (lookJoystick.joystickVec.y != 0 || lookJoystick.joystickVec.x != 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
        }
    }
    public void PlayerDied()
    {
        Debug.Log("Gameover!");
        // switch to death screen
        playerHealth.OnDeathEvent.RemoveListener(PlayerDied); // unregisters function as listener
    }
}