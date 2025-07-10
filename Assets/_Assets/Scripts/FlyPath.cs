using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lưu trữ danh sách các waypoint mà FlyPathAgent sẽ đi qua
public class FlyPath : MonoBehaviour
{
    public Waypoint[] waypoints;   // Mảng các waypoint (các điểm bay, thường là các GameObject con có gắn component Waypoint)

    // Indexer cho phép truy cập trực tiếp vị trí của waypoint qua chỉ số
    public Vector3 this[int index] => waypoints[index].transform.position;

    // Hàm chỉ chạy trong Unity Editor khi component được gắn hoặc reset
    // Tự động lấy tất cả các Waypoint là con của GameObject này
    private void Reset() => waypoints = GetComponentsInChildren<Waypoint>();

    // Vẽ đường nối giữa các waypoint trong Scene view (chỉ hoạt động trong Editor)
    private void OnDrawGizmos()
    {
        if (waypoints == null) return; // Nếu chưa có waypoint thì không vẽ gì

        Gizmos.color = Color.green;    // Đặt màu vẽ là xanh lá

        // Vẽ từng đoạn thẳng nối các waypoint liên tiếp
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(
                waypoints[i].transform.position,
                waypoints[i + 1].transform.position
            );
        }
    }
}
