using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;         // Tốc độ tối đa
    [SerializeField] private float joystickRadius = 100f;   // Bán kính joystick ảo (tính bằng pixel)

    private Camera mainGameCamera;          // Camera chính
    private Vector2 startTouchPosition;     // Vị trí bắt đầu chạm
    private Vector2 currentDirection;       // Hướng di chuyển
    private float currentSpeed = 0f;        // Tốc độ hiện tại phụ thuộc độ kéo
    private bool isTouching = false;        // Cờ đang chạm

    void Awake()
    {
        mainGameCamera = Camera.main;
        if (mainGameCamera == null)
        {
            Debug.LogError("Không tìm thấy Main Camera. Gán tag 'MainCamera' cho camera.");
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        HandleMouseInput();
#else
        HandleTouchInput();
#endif

        if (isTouching)
        {
            Vector3 newPos = transform.position + (Vector3)(currentDirection * currentSpeed * Time.deltaTime);
            MovePlayerClamped(newPos);
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
            isTouching = true;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 currentPosition = Input.mousePosition;
            Vector2 delta = currentPosition - startTouchPosition;

            float distance = Mathf.Min(delta.magnitude, joystickRadius);         // Giới hạn kéo trong bán kính
            currentDirection = delta.normalized;
            currentSpeed = moveSpeed * (distance / joystickRadius);             // Tốc độ tỷ lệ độ kéo
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isTouching = false;
            currentDirection = Vector2.zero;
            currentSpeed = 0f;
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 pos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = pos;
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                Vector2 delta = pos - startTouchPosition;

                float distance = Mathf.Min(delta.magnitude, joystickRadius);
                currentDirection = delta.normalized;
                currentSpeed = moveSpeed * (distance / joystickRadius);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
                currentDirection = Vector2.zero;
                currentSpeed = 0f;
            }
        }
    }

    void MovePlayerClamped(Vector3 targetPosition)
    {
        // Dùng khoảng cách z > 0 để tránh lỗi ScreenToWorldPoint trả sai
        float z = Mathf.Abs(mainGameCamera.transform.position.z);
        Vector3 minBounds = mainGameCamera.ViewportToWorldPoint(new Vector3(0, 0, z));
        Vector3 maxBounds = mainGameCamera.ViewportToWorldPoint(new Vector3(1, 1, z));

        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);
        targetPosition.z = 0;

        transform.position = targetPosition;
    }
}
