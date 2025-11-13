using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference abilityAction;
    public InputActionReference characterSwap;

    public float moveSpeed = 5f;
    public float jumpForce = 500f;
    public Rigidbody2D rb;
    private Vector2 moveInput;

    [SerializeField] bool isGrounded = true;
    [SerializeField] private bool activeAbility = false;
    [SerializeField] float groundCheckDistance = 5f;
    private SpriteRenderer characterColor;

    enum CurrentState { NoAbility, AntigravActive, ShrinkActive, Jumping }
    [SerializeField] CurrentState state = CurrentState.NoAbility;

    enum CurrentCharacter { CharacterA, CharacterB, CharacterC };
    [SerializeField] CurrentCharacter character = CurrentCharacter.CharacterA;

    private void Start()
    {
        characterColor = this.GetComponent <SpriteRenderer> ();
    }
    // Update is called once per frame
    void Update()
    {
        if (moveAction != null) 
        {
            moveInput = moveAction.action.ReadValue<Vector2>();
        }
        HandleState();
        HandleCharacter();

        //Jump(); //If this goes here the second I'm grounded it thinks I'm jumping state
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void OnEnable()
    {
        if (moveAction != null) moveAction.action.Enable();

        if (jumpAction != null)
        {
            jumpAction.action.performed += Jump;
           // jumpAction.action.canceled += OnJumpReleased;
            jumpAction.action.Enable();
        }
        if (characterSwap != null)
        { 
            characterSwap.action.performed += OnSwapPressed;
            characterSwap.action.canceled += OnSwapReleased;
            characterSwap.action.Enable();
        }
        if (abilityAction != null)
        {
            abilityAction.action.performed += UseAbility;
            abilityAction.action.Enable();
        }
    }
    private void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();

        if (jumpAction != null)
        {
            jumpAction.action.performed += Jump;
          //  jumpAction.action.canceled += OnJumpReleased;
            jumpAction.action.Disable();
        }
        if (characterSwap != null)
        {
            characterSwap.action.performed += OnSwapPressed;
            characterSwap.action.canceled += OnSwapReleased;
            characterSwap.action.Disable();
        }
        if (abilityAction != null)
        {
            abilityAction.action.performed += UseAbility;
            abilityAction.action.Disable();
        }
    }

    private void OnSwapPressed(InputAction.CallbackContext callbackContext)
    {
        Cycle();
        Debug.Log("Swapped");
       
    }

    private void OnSwapReleased(InputAction.CallbackContext callbackContext)
    {
      
    }

    public void Cycle()
    {
        if (character == CurrentCharacter.CharacterA)  //&& character != CurrentCharacter.CharacterA
        {
            character = CurrentCharacter.CharacterB;
            Debug.Log("B");
        }
        else if (character == CurrentCharacter.CharacterB)
        {
            character = CurrentCharacter.CharacterC;
            Debug.Log("C");
        }
        else if (character == CurrentCharacter.CharacterC)
        {
            character = CurrentCharacter.CharacterA;
            Debug.Log("A");
        }
    }

   /* void OnJumpPressed(InputAction.CallbackContext callbackContext) //A way to call jump through a debug!
    {
        Debug.Log("Space Pressed");
        Jump();
    } */

    /* void OnJumpReleased(InputAction.CallbackContext callbackContext) //Same as above but released :)
    {
           Debug.Log("Space Released");
    } */ 

    private void FixedUpdate()
    {
        if (moveInput.sqrMagnitude > 1) moveInput.Normalize();

        float stepx = moveInput.x * moveSpeed;
        Vector2 step = new Vector2(stepx, 0);
       // rb.MovePosition(rb.position + step); //This is left/right movement but deactivates the jump on first frame
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        RaycastHit2D raycastHit;
        raycastHit = Physics2D.Raycast(transform.position + (Vector3.down * 0.51f), Vector2.down, groundCheckDistance);
        Debug.DrawRay(transform.position, Vector2.down, Color.red, groundCheckDistance); //This makes the laser appear
       if (raycastHit.collider != null && raycastHit.collider.gameObject.layer != 3)
        {
            Debug.Log(raycastHit.collider.gameObject);
            isGrounded = true;
           // state = CurrentState.DefaultMovement; //< This makes it so you can't activate your ability
        }
        else
        {
            isGrounded = false;
            state = CurrentState.Jumping; //< if the above comment is deactivated then you stay in jump
        } 
       
    }

    public void Jump(InputAction.CallbackContext callbackContext) //All working!
    { 
         if (rb == null) return;
         if (!isGrounded) return;

        Debug.Log("You jumped!");

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
      //  state = CurrentState.Jumping;
    }

    public void UseAbility(InputAction.CallbackContext callbackContext)
    {
        // add grounded check here? So if you click ability you stay in it?? EG. && isGrounded == true
        
       // if (character == CurrentCharacter.CharacterA && isGrounded == true) { state = CurrentState.DefaultMovement; }
        if (character == CurrentCharacter.CharacterB && activeAbility == false) //REMEMBER THE DOUBLE EQUALS
        {
            state = CurrentState.ShrinkActive;
        }
        else if (character == CurrentCharacter.CharacterB && activeAbility == true)
        {
            state = CurrentState.NoAbility;
        }
        else if (character == CurrentCharacter.CharacterC && activeAbility == false)
        {
            state = CurrentState.AntigravActive;
        }
        else if (character == CurrentCharacter.CharacterC && activeAbility == true)
        {
            state = CurrentState.NoAbility;
        }
    }

    private void HandleState() //Keeping track of the kinds of powers you can use... like if you're jumping you cannot be jumping again until you're grounded
        // used with abilityactive bool to make sure you can't do overlap stuff
        // cases are MAINLY just to put things in nice sections so its not all messy
    {
        switch (state)
        {
            case CurrentState.NoAbility:
                activeAbility = false;
                break;
            case CurrentState.AntigravActive:
                activeAbility = true;
                break;
            case CurrentState.ShrinkActive:
                activeAbility = true;
                break;
           // case CurrentState.Jumping:
                
                //break;
        }
    }

    private void HandleCharacter() //Keeping track of what form you're in!
    {
        switch (character)
        {
            case CurrentCharacter.CharacterA: //default and jump
                characterColor.color = Color.indianRed;
                if (isGrounded)
                {
                    state = CurrentState.NoAbility;
                }
                break;
            case CurrentCharacter.CharacterB: //shrink
                characterColor.color = Color.darkOrchid;
                break;
            case CurrentCharacter.CharacterC: //antigrav
                characterColor.color = Color.royalBlue;
                break;
        }
    }



}
