using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script điều khiển đối tượng bay theo các điểm định sẵn (waypoints)
public class FlyPathAgent : MonoBehaviour
{
    public Transform flyPath;         // Đối tượng chứa các waypoint (các Transform con)
    public float flySpeed = 2f;       // Tốc độ bay
    private int nextIndex = 0;        // Index của điểm kế tiếp

    void Update()
    {
        if (flyPath == null || flyPath.childCount == 0) return;

        if (nextIndex >= flyPath.childCount)
        {
            Destroy(gameObject); // Hết đường bay thì huỷ đối tượng
            return;
        }

        Transform target = flyPath.GetChild(nextIndex);
        Vector3 targetPos = target.position;

        // Nếu chưa tới target thì bay tới
        if (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            FlyToNextWaypoint(targetPos);
            LookAt(targetPos);
        }
        else
        {
            nextIndex++; // Đến nơi thì tăng chỉ số điểm tiếp theo
        }
    }

    private void FlyToNextWaypoint(Vector3 destination)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            destination,
            flySpeed * Time.deltaTime
        );
    }

    private void LookAt(Vector3 destination)
    {
        Vector3 dir = destination - transform.position;
        if (dir.magnitude < 0.01f) return;

        float angle = Vector2.SignedAngle(Vector2.down, dir); // Xoay theo hướng bay
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
