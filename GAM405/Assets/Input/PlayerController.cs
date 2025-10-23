using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public float moveSpeed = 5f;
    public float jumpForce = 500f;
    public Rigidbody2D rb;
    private Vector2 moveInput;
    [SerializeField] bool isGrounded = true;
    [SerializeField] private bool activeAbility = false;
    [SerializeField] float groundCheckDistance = 5f;


    enum CurrentState { DefaultMovement, AntigravActive, AttackActive, ShrinkActive, Jumping }
    [SerializeField] CurrentState state = CurrentState.DefaultMovement;

    enum CurrentCharacter { CharacterA, CharacterB, CharacterC };
    [SerializeField] CurrentCharacter character = CurrentCharacter.CharacterA;

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
            jumpAction.action.performed += OnJumpPressed;
            jumpAction.action.canceled += OnJumpReleased;
            jumpAction.action.Enable();
        }
    }
    private void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();

        if (jumpAction != null)
        {
            jumpAction.action.performed += OnJumpPressed;
            jumpAction.action.canceled += OnJumpReleased;
            jumpAction.action.Disable();
        }
    }

    void OnJumpPressed(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Space Pressed");

        Jump();

    }

    void OnJumpReleased(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Space Released");
    }

    private void FixedUpdate()
    {
        if (moveInput.sqrMagnitude > 1) moveInput.Normalize();

        float stepx = moveInput.x * moveSpeed;
        Vector2 step = new Vector2(stepx, 0);
       // rb.MovePosition(rb.position + step); //This is left/right movement //THIS IS EVIL
       //rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        RaycastHit2D raycastHit;
        raycastHit = Physics2D.Raycast(transform.position + (Vector3.down * 0.51f), Vector2.down, groundCheckDistance);
        Debug.DrawRay(transform.position, Vector2.down, Color.red, groundCheckDistance); //This makes the laser appear
       if (raycastHit.collider != null && raycastHit.collider.gameObject.layer != 3)
        {
            Debug.Log(raycastHit.collider.gameObject);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        } 

       
    }

    public void Jump()
    {


        // if (Input.GetKeyDown(KeyCode.Space)) 
        // if (rb == null) return;
        //  if (!isGrounded) return;

        Debug.Log("You jumped!"); // WHY DOES ONLY THIS TRIGGER???????

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        state = CurrentState.Jumping;
    }

    private void HandleState() //Keeping track of the kinds of powers you can use... like if you're jumping you cannot be jumping again until you're grounded
        // used with abilityactive bool to make sure you can't do overlap stuff
        // cases are MAINLY just to put things in nice sections so its not all messy
    {
        switch (state)
        {
            case CurrentState.DefaultMovement:
                activeAbility = false;
                break;
            case CurrentState.AntigravActive:
                activeAbility = true;
                break;
            case CurrentState.AttackActive:
                activeAbility = true;
                break;
            case CurrentState.ShrinkActive:
                activeAbility = true;
                break;
            case CurrentState.Jumping:
                activeAbility = false;
                break;
        }
    }

    private void HandleCharacter() //Keeping track of what form you're in!
    {
        switch (character)
        {
            case CurrentCharacter.CharacterA:
                break;
            case CurrentCharacter.CharacterB:
                break;
            case CurrentCharacter.CharacterC:
                break;
        }
    }



}
