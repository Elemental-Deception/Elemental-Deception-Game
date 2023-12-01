using UnityEngine;

public class BossBullet : MonoBehaviour
{
    void Start()
    {
        // Destroy the projectile after 2 seconds
        Destroy(gameObject, 2f);
    }

    // Add any additional behavior for the projectile here, such as collision handling
}
