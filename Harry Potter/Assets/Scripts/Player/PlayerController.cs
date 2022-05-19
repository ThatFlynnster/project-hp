using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public BaseStats stats;

    [SerializeField]
    private float playerSpeed = 3.0f;
    [SerializeField]
    private float sprintMultiplier = 1.0f;
    [SerializeField]
    private float jumpHeight = 0.5f;
    [SerializeField]
    private float rotationSpeed = 30f;
    [SerializeField]
    private AnimationCurve dashCurve;
    [SerializeField]
    private float dashSpeed = 3.0f;
    [SerializeField]
    private float dashMaxTime = 0.25f;
    private float dashTime = 0f;
    [SerializeField]
    private float sprintTime = 0;
    public AnimationCurve sprintCurve;

    private float gravityValue = -9.81f;
    private float normalTargetRotation;
    private float turnSmoothVelocity;

    [SerializeField]
    private GameObject attackPrefab;
    [SerializeField]
    private Transform wandTipTransform;
    [SerializeField]
    private Transform attackParent;
    private float attackDuration;


    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private Vector3 moveDir;
    private bool isGrounded;
    public Transform cameraYRot;
    public Transform cameraTransform;

    private bool isDueling = false;
    private bool isMoving;
    private bool isSprinting;

    private InputAction duelAction;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction dodgeAction;
    private InputAction attackAction;


    public float targetRotation = 0f;

    #region Setup
    void Awake()
    {
        cameraTransform = Camera.main.transform;
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        duelAction = playerInput.actions["Duel"];
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        sprintAction = playerInput.actions["Sprint"];
        dodgeAction = playerInput.actions["Dodge"];
        attackAction = playerInput.actions["Attack"];
    }

    void Start()
    {
        attackDuration = stats.atkDuration;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
    #endregion

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded) playerVelocity.y = -1f;

        InputCheck();
        Move();
        Jump();
        Rotate();
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    #region Input Check
    private void InputCheck()
    {
        moveAction.performed += context => isMoving = true;
        moveAction.canceled += _ => isMoving = false;

        sprintAction.performed += context => isSprinting = true;
        sprintAction.canceled += _ => isSprinting = false;
    }

    private void OnEnable()
    {
        duelAction.performed += _ => DuelToggle();
        attackAction.performed += _ => Attack();
        dodgeAction.performed += _ => DodgeCheck();
    }

    private void OnDisable()
    {
        duelAction.performed -= _ => DuelToggle();
        attackAction.performed -= _ => Attack();
        dodgeAction.performed -= _ => DodgeCheck();
    }
    #endregion

    private void DuelToggle()
    {
        if (isDueling) isDueling = false;
        else isDueling = true;
    }

    private void Attack()
    {
        RaycastHit hit;
        GameObject attack = GameObject.Instantiate(attackPrefab, wandTipTransform.position, Quaternion.identity, attackParent);
        BulletController bulletController = attack.GetComponent<BulletController>();
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
        {
            bulletController.target = hit.point;
            bulletController.hit = true;
        }
        else
        {
            bulletController.target = cameraTransform.position + cameraTransform.forward * attackDuration;
            bulletController.hit = false;
        }
    }

    private void DodgeCheck()
    {
        if (!isGrounded) return;
        StartCoroutine(Dodge());
    }

    IEnumerator Dodge()
    {
        float startTime = Time.time;
        dashTime = 0f;
        Vector3 move = moveDir;

        while (Time.time < startTime + dashMaxTime)
        {
            dashTime += Time.deltaTime;
            dashSpeed = dashCurve.Evaluate(dashTime);

            controller.Move(move * dashSpeed * Time.deltaTime);

            yield return null;
        }
    }


    private void Move()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraYRot.right.normalized + move.z * cameraYRot.forward.normalized;
        move.y = 0f;
        moveDir = move;

        //Adventure Mode Rotation Values
        if (isMoving) targetRotation = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
        else targetRotation = transform.eulerAngles.y;
        normalTargetRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, rotationSpeed / 200f);

        Sprint();

        controller.Move(move * Time.deltaTime * playerSpeed * sprintMultiplier);
    }

    private void Sprint()
    {
        if (!isMoving)
        {
            sprintTime = 0f;
            return;
        }

        if (isSprinting) sprintTime += Time.deltaTime * 2.0f;
        else sprintTime -= Time.deltaTime * 1.5f;

        if (sprintTime < 0) sprintTime = 0f;
        if (sprintTime > 1) sprintTime = 1f;

        sprintMultiplier = sprintCurve.Evaluate(sprintTime);
    }

    private void Jump()
    {
        if (!isGrounded) return;
        if (jumpAction.triggered)
        {
            Vector3 move = moveDir;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            //controller.Move(move * Time.deltaTime * playerSpeed * sprintMultiplier);
        }
    }

    private void Rotate()
    {
        if (isDueling)
        {
            Quaternion targetRotation = Quaternion.Euler(0, cameraYRot.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else 
        {
            transform.rotation = Quaternion.Euler(0f, normalTargetRotation, 0f);
        }
    }
}