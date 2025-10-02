using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionReference moveAction;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
  
    // Update is called once per frame
    void Update()
    {
        if (moveAction != null) 
        {
            moveInput = moveAction.action.ReadValue<Vector2>();
        }
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
        if (moveAction !=null) moveAction.action.Disable(); 
    }
    private void FixedUpdate()
    {
        if (moveInput.sqrMagnitude > 1) moveInput.Normalize();

        Vector2 step = moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + step);
    }
}
