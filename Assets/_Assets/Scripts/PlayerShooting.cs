using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefabs;  
    public float shootingInterval;
    public Vector3 bulletOffset;

    private float lastBulletTime;
    void Update()
    {
        UpdateFiring(); 
    }

    void UpdateFiring()
    {
        if (Time.time - lastBulletTime > shootingInterval)
        { 
            ShootBullet();
            lastBulletTime = Time.time;
        }
    }

    private void ShootBullet()
    {
        Instantiate(bulletPrefabs, transform.position + bulletOffset, transform.rotation);
    }
}
