using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script điều khiển đối tượng bay theo các điểm định sẵn (waypoints)
public class FlyPathAgent : MonoBehaviour
{
    public FlyPath flyPath;            // Tham chiếu đến đối tượng chứa các waypoint (điểm bay)
    public float flySpeed;             // Tốc độ bay của đối tượng
    private int nextIndex = 1;         // Chỉ số của waypoint kế tiếp cần bay tới (bắt đầu từ 1)

    void Update()
    {
        if (flyPath == null) return;                                   // Nếu chưa gán flyPath thì thoát
        if (nextIndex >= flyPath.waypoints.Length) 
        {
            Destroy(gameObject);
            return;
        }           

        if (transform.position != flyPath[nextIndex])                  // Nếu chưa đến waypoint kế tiếp
        {
            FlyToNextWaypoint();                                       // Di chuyển đến waypoint kế tiếp
            LookAt(flyPath[nextIndex]);                                // Xoay đối tượng để nhìn hướng bay
        }
        else
        {
            nextIndex++;                                               // Khi đến waypoint, chuyển sang waypoint kế tiếp
        }
    }

    // Hàm di chuyển đối tượng dần về waypoint kế tiếp
    private void FlyToNextWaypoint()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            flyPath[nextIndex],
            flySpeed * Time.deltaTime
        );
    }

    // Hàm xoay đối tượng để nhìn theo hướng bay
    private void LookAt(Vector2 destination)
    {
        Vector2 position = transform.position;                         // Lấy vị trí hiện tại
        var lookDirection = destination - position;                    // Tính vector hướng đến điểm đích

        if (lookDirection.magnitude < 0.01f) return;                   // Nếu gần như không có hướng (đã đến đích) thì không xoay

        var angle = Vector2.SignedAngle(Vector3.down, lookDirection); // Tính góc xoay từ hướng xuống (mặc định) đến hướng bay
        transform.rotation = Quaternion.Euler(0, 0, angle);            // Xoay đối tượng theo góc tính được
    }
}
