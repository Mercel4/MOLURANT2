using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class SimpleFirstPersonController : MonoBehaviourPun
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float jumpHeight = 1.4f;
    public float gravity = -9.81f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 2.0f;
    public float gamepadSensitivity = 150f;
    public float maxLookAngle = 85f;

    private CharacterController cc;
    private float verticalVelocity = 0f;
    private float cameraPitch = 0f;
    private Transform playerCamera;

    // ─── New Input System ───────────────────────────────
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction runAction;
    // ────────────────────────────────────────────────────

    void Awake()
    {
        moveAction = new InputAction("Move");
        moveAction.AddCompositeBinding("2DVector")
            .With("Up",    "<Keyboard>/w")
            .With("Down",  "<Keyboard>/s")
            .With("Left",  "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        moveAction.AddBinding("<Gamepad>/leftStick");

        lookAction = new InputAction("Look", binding: "<Mouse>/delta");
        lookAction.AddBinding("<Gamepad>/rightStick");

        jumpAction = new InputAction("Jump", binding: "<Keyboard>/space");
        jumpAction.AddBinding("<Gamepad>/buttonSouth"); // 게임패드 A버튼 (Xbox) / X버튼 (PS)

        runAction = new InputAction("Run", binding: "<Keyboard>/leftShift");
        runAction.AddBinding("<Gamepad>/leftStickPress"); // 왼쪽 스틱 누르기 (L3)
    }

    void Start()
    {
        cc = GetComponent<CharacterController>();

        if (photonView.IsMine)
        {
            moveAction.Enable();
            lookAction.Enable();
            jumpAction.Enable();
            runAction.Enable();

            playerCamera = Camera.main.transform;
            playerCamera.SetParent(transform);
            playerCamera.localPosition = new Vector3(0, 0.75f, 0);
            playerCamera.localRotation = Quaternion.identity;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void OnDestroy()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        runAction.Disable();
    }

    void Update()
    {
        if (!photonView.IsMine) return;
        if (StoreUI.isStoreOpen) return;

        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        if (playerCamera == null) return;

        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        bool isGamepad = Gamepad.current != null &&
                         Gamepad.current.rightStick.IsActuated();

        float sensitivity = isGamepad
            ? gamepadSensitivity * Time.deltaTime
            : mouseSensitivity;

        Vector2 lookDelta = lookInput * sensitivity;

        transform.Rotate(Vector3.up * lookDelta.x);
        cameraPitch -= lookDelta.y;
        cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle);
        playerCamera.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);
    }

    void HandleMovement()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        bool isRunning = runAction.IsPressed();
        float speed = isRunning ? runSpeed : walkSpeed;

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        move = move.normalized * speed;

        if (cc.isGrounded)
        {
            if (verticalVelocity < 0f) verticalVelocity = -2f;

            if (jumpAction.WasPressedThisFrame())
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;
        cc.Move(move * Time.deltaTime);

        Gun.isRunning = !(moveInput.x == 0 && moveInput.y == 0 && cc.isGrounded);
    }

    public void AddRecoil(float amount)
    {
        cameraPitch -= amount;
        cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle);
        if (playerCamera != null)
            playerCamera.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);
    }

    public void UnlockCursor()
    {
        if (!photonView.IsMine) return;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}