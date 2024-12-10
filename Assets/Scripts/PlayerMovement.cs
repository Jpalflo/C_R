using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    //components
    [SerializeField]private CharacterController characterController;
    [SerializeField]private Transform cameraTransform;

    //movement and jump config parameters
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = Physics.gravity.y;

    //public Image staminaBar; //mio

    //Input fields for move and look actions
    private Vector2 moveInput;
    private Vector2 lookInput;

    //velocity and rotation var
    private Vector2 velocity;
    private float verticalVelocity;
    private float verticalRotation;

    //is sprinting state
    private bool isSprinting;

    //camera look sense and max angle to limit v rotation
    [SerializeField] private float lookSensitivity = 5f;
    private float maxLookAngle = 80f;

    //[SerializeField] private float stamina; //mio
    //[SerializeField] private float maxStamina;


    ////Stamina
    //[Header("Stamina")]
    //[SerializeField] private float maxStamina = 100f;
    //[SerializeField] private float staminaDrainRate = 10f;
    //[SerializeField] private float staminaRegenRate = 5f;
    //private float currentStamina;
    private bool isMoving;

    [SerializeField] private GameObject colliderAttack;

    private Animator animator;
    public int puntos;

    public void Awake()
    {
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        colliderAttack = transform.GetChild(0).gameObject;

        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        //inicializar barr to max
        //currentStamina = maxStamina;
        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        this.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        LookAround();



        Debug.Log(puntos);
    }

    /// <summary>
    /// receives move IS
    /// </summary>
    /// <param name="context"></param>
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        isMoving = moveInput != Vector2.zero;
    }
    /// <summary>
    /// receive look input from inpout system
    /// </summary>
    /// <param name="context"></param>
    public void Look(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// receive jump IS and triggers jump if grounded
    /// </summary>
    /// <param name="context"></param>
    public void Jump(InputAction.CallbackContext context)
    {
        //if player is touching ground
        if (characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    /// <summary>
    /// receive Sprint from IS and change isSprinting state
    /// </summary>
    /// <param name="context"></param>
    public void Sprint(InputAction.CallbackContext context)
    {
        isSprinting = context.started || context.performed;

        if (context.started || context.performed)
        {
            speed = 10;
            //CheckStamina(); //mio

        }
        if (context.canceled)
        {
            speed = 5;
        }
    }

    /// <summary>
    /// move jump player
    /// </summary>
    private void MovePlayer()
    {
        //falling down
        if (characterController.isGrounded)
        {
            //restart v velocity when touchs ground
            verticalVelocity = 0f;
        }
        else
        {
            //
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 move = new Vector3(0, verticalVelocity, 0);
        characterController.Move(move * Time.deltaTime);

        //movement
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        if (moveInput.x > 0 || moveInput.y > 0) 
        {
            animator.SetBool("Run", true);
        } 
        else
        {
            animator.SetBool("Run", false);
        }
        moveDirection = transform.TransformDirection(moveDirection);
        float targetSpeed = isSprinting ? speed * speedMultiplier : speed;
        characterController.Move(moveDirection * targetSpeed * Time.deltaTime);

        //apply gravity constantly
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    /// <summary>
    /// camera rotation
    /// </summary>
    private void LookAround()
    {
        //horizontal rotation 
        float horizontalRotation = lookInput.x * lookSensitivity;
        transform.Rotate(Vector3.up * horizontalRotation);

        //vertical  rotation xaxis
        verticalRotation -= lookInput.y * lookSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
    

    /// <summary>
    /// attack method
    /// </summary>
    /// <param name="context"></param>
    public void AttackEnemy(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            colliderAttack.SetActive(true);
        }
        else
        {
            colliderAttack.SetActive(false);

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Destroy(other.gameObject);
            puntos += 250;
        }
    }
}
