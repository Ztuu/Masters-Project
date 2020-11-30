using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //States
    private State currentState;
    private IdleState idleState;
    private RunState runState;
    private JumpState jumpState;
    private FiringState shootState;
    
    //Components
    public Rigidbody2D AttachedRigidBody { get; private set;}
    public SpriteRenderer PlayerSprite { get; private set; }
    public Animator PlayerAnimator { get; private set; }
    public AudioSource PlayerAudio { get; private set; }

    //SFX
    public AudioClip shootSound, jumpSound;

    private int maxHealth=1, currentHealth;

    public delegate void PlayerEventHandler();
    public event PlayerEventHandler Death;

    void Start()
    {

        //Initialise states
        idleState = gameObject.AddComponent<IdleState>();
        runState = gameObject.AddComponent<RunState>();
        jumpState = gameObject.AddComponent<JumpState>();
        shootState = gameObject.AddComponent<FiringState>();

        State.AssignPlayer(this);

        AttachedRigidBody = GetComponent<Rigidbody2D>();
        PlayerSprite = GetComponentInChildren<SpriteRenderer>();
        PlayerAnimator = GetComponentInChildren<Animator>();
        PlayerAudio = GetComponent<AudioSource>();
        Reset();
    }

    //Called once each frame
    void Update() { 
        //Capture inputs
        PlayerInput thisFrameInput = new PlayerInput();
        thisFrameInput.HorizontalInput = Input.GetAxisRaw("Horizontal");
        thisFrameInput.JumpInput = Input.GetButtonDown("Jump");
        thisFrameInput.FireInput = Input.GetButtonDown("Fire1");

        //Have current state handle inputs
        currentState.HandleInput(thisFrameInput);
    }

    public void ChangeState(string newState)     
    {
        switch (newState)
        {
            case "idle":
                currentState = idleState;
                break;
            case "run":
                currentState = runState;
                break;
            case "jump":
                currentState = jumpState;
                break;
            case "shoot":
                currentState = shootState;
                break;
            default:
                Debug.Log(newState + " is not a valid state for the player to change to");
                break;
        }
        currentState.Entry(); //Assuming we always change state when calling this method
    }

    public void Reset()
    {
        currentState = idleState;
        currentHealth = maxHealth;
        //Reset animator to idle state
        PlayerAnimator.SetBool("running", false);
        PlayerAnimator.SetBool("landed", true);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Reset();
            Death();
        }
    }
}
