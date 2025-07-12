using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // Tốc độ di chuyển của máy bay
    private bool isPlayerDragging = false;          // Cờ kiểm tra xem người chơi có đang kéo máy bay không
    private Vector3 dragOffset;                     // Khoảng cách giữa vị trí máy bay và vị trí kéo
    private Camera mainGameCamera;                  // Camera chính để chuyển đổi tọa độ màn hình về thế giới

    void Awake()
    {
        mainGameCamera = Camera.main; // Gán camera chính từ tag "MainCamera"
        if (mainGameCamera == null)
        {
            Debug.LogError("PlayerMovement: Không tìm thấy Camera chính (MainCamera). Hãy kiểm tra Tag của Camera trong scene.");
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        // --- Điều khiển bằng CHUỘT trong Unity Editor ---
        if (Input.GetMouseButtonDown(0)) // Khi nhấn chuột trái
        {
            Vector3 mouseWorldPos = mainGameCamera.ScreenToWorldPoint(Input.mousePosition); // Lấy vị trí chuột trong thế giới
            mouseWorldPos.z = 0; // Gán Z = 0 vì game là 2D

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero); // Raycast tại vị trí chuột
            if (hit.collider != null && hit.collider.gameObject == gameObject) // Nếu raycast trúng chính GameObject này
            {
                isPlayerDragging = true; // Bắt đầu kéo
                dragOffset = transform.position - mouseWorldPos; // Tính khoảng cách giữa chuột và máy bay
            }
        }
        else if (Input.GetMouseButton(0) && isPlayerDragging) // Khi đang giữ chuột và đã bắt đầu kéo
        {
            Vector3 mouseWorldPos = mainGameCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            Vector3 targetPosition = mouseWorldPos + dragOffset; // Vị trí đích sau khi cộng offset
            MovePlayerClamped(targetPosition); // Di chuyển tới vị trí đích
        }
        else if (Input.GetMouseButtonUp(0)) // Khi nhả chuột ra
        {
            isPlayerDragging = false; // Dừng kéo
        }
#else
        // --- Điều khiển bằng CẢM ỨNG trên thiết bị di động ---
        if (Input.touchCount > 0) // Có ít nhất một ngón tay chạm
        {
            Touch firstTouch = Input.GetTouch(0); // Lấy ngón tay đầu tiên
            Vector3 touchWorldPos = mainGameCamera.ScreenToWorldPoint(firstTouch.position);
            touchWorldPos.z = 0;

            if (firstTouch.phase == TouchPhase.Began) // Khi bắt đầu chạm
            {
                RaycastHit2D hit = Physics2D.Raycast(touchWorldPos, Vector2.zero); // Raycast tại điểm chạm
                if (hit.collider != null && hit.collider.gameObject == gameObject) // Nếu trúng máy bay
                {
                    isPlayerDragging = true;
                    dragOffset = transform.position - touchWorldPos;
                }
            }
            else if (firstTouch.phase == TouchPhase.Moved && isPlayerDragging) // Nếu ngón tay đang di chuyển và đang kéo
            {
                Vector3 targetPosition = touchWorldPos + dragOffset;
                MovePlayerClamped(targetPosition); // Di chuyển máy bay theo ngón tay
            }
            else if (firstTouch.phase == TouchPhase.Ended || firstTouch.phase == TouchPhase.Canceled) // Khi ngón tay nhấc ra
            {
                isPlayerDragging = false; // Dừng kéo
            }
        }
#endif
    }

    // Di chuyển máy bay tới vị trí đích nhưng giới hạn trong màn hình
    void MovePlayerClamped(Vector3 targetPosition)
    {
        // Lấy giới hạn màn hình theo tọa độ thế giới
        Vector3 minBounds = mainGameCamera.ViewportToWorldPoint(new Vector3(0, 0, mainGameCamera.nearClipPlane)); // Góc dưới bên trái
        Vector3 maxBounds = mainGameCamera.ViewportToWorldPoint(new Vector3(1, 1, mainGameCamera.nearClipPlane)); // Góc trên bên phải

        // Giới hạn vị trí không cho vượt ra ngoài màn hình
        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);
        targetPosition.z = 0; // Z phải bằng 0 vì đang dùng 2D

        // Di chuyển máy bay mượt mà tới vị trí đích
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
