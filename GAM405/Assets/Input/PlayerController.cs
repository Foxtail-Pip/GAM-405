using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{
    public InputActionReference moveAction;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    [SerializeField] bool isGrounded = true;
    [SerializeField] private bool activeAbility = false;
    [SerializeField] float groundCheckDistance = 5f;

    enum CurrentState { CharacterA, CharacterB, CharacterC, AntigravActive, AttackActive, ShrinkActive, Jumping }
    [SerializeField] CurrentState state = CurrentState.CharacterA;

    // Update is called once per frame
    void Update()
    {
        if (moveAction != null) 
        {
            moveInput = moveAction.action.ReadValue<Vector2>();
        }
        HandleState();
        // Jump(); If this goes here the second I'm grounded it thinks I'm jumping state
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void OnEnable()
    {
        if (moveAction != null) moveAction.action.Enable(); 

    }
    private void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();
    }

    private void FixedUpdate()
    {
        if (moveInput.sqrMagnitude > 1) moveInput.Normalize();

        float stepx = moveInput.x * moveSpeed * Time.fixedDeltaTime;
        Vector2 step = new Vector2(stepx, 0);
        rb.MovePosition(rb.position + step); //This is left/right movement

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
        if (rb == null) return;
        if (!isGrounded) return;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        state = CurrentState.Jumping;
    }

    private void HandleState()
    {
        switch (state)
        {
            case CurrentState.CharacterA:
                break;
            case CurrentState.CharacterB:
                break;
            case CurrentState.CharacterC:
                break;
            case CurrentState.AntigravActive:
                break;
            case CurrentState.AttackActive: 
                break;
            case CurrentState.ShrinkActive:
                break;
            case CurrentState.Jumping:
                break;
        }
    }
}
