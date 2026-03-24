using UnityEngine;
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
    public float maxLookAngle = 85f;

    private CharacterController cc;
    private float verticalVelocity = 0f;
    private float cameraPitch = 0f;

    private Transform playerCamera; // 🔹 수정: 내 캐릭터 전용 카메라 저장용

    void Start()
    {
        cc = GetComponent<CharacterController>();

        // 🔹 수정: 내 캐릭터일 때만 카메라 세팅
        if (photonView.IsMine)
        {
            // 메인 카메라 찾아서 내 캐릭터 자식으로 설정
            playerCamera = Camera.main.transform;
            playerCamera.SetParent(transform);
            playerCamera.localPosition = new Vector3(0, 0.75f, 0);
            playerCamera.localRotation = Quaternion.identity;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            // 🔹 수정: 내 캐릭터가 아닐 경우 카메라 제거
            if (Camera.main != null && Camera.main.transform.IsChildOf(transform))
            {
                Camera.main.transform.SetParent(null);
            }
        }
    }

    void Update()
    {
        // 🔹 수정: 내 캐릭터가 아니면 입력, 카메라 처리 전부 패스
        if (!photonView.IsMine) return;

        // 🔹 수정: 상점 열려있으면 조작 막기
        if (StoreUI.isStoreOpen)
        {
            return;
        }

        HandleMouseLook();
        HandleMovement();
    }

    public void AddRecoil(float amount)
    {
        // 위로 들리게 (Y 마우스 이동과 같은 방향)
        cameraPitch -= amount;
        cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle);
        if (playerCamera != null)
            playerCamera.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);
    }

    void HandleMouseLook()
    {
        if (playerCamera == null) return;
        // 

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 좌우 회전 (몸통 회전)
        transform.Rotate(Vector3.up * mouseX);

        // 상하 회전 (카메라 피치 조정)
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle);
        playerCamera.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);
    }

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runSpeed : walkSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        move = move.normalized * speed;

        if (cc.isGrounded)
        {
            if (verticalVelocity < 0f)
                verticalVelocity = -2f; // 살짝 눌러줌 (안 끌어내려가게)

            if (Input.GetButtonDown("Jump"))
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;
        cc.Move(move * Time.deltaTime);

        // 🔹 수정: 이동 상태를 다른 스크립트에 전달할 때 static 참조 최소화
        if (x == 0 && z == 0 && cc.isGrounded)
            Gun.isRunning = false;
        else
            Gun.isRunning = true;
    }

    public void UnlockCursor()
    {
        if (!photonView.IsMine) return;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
