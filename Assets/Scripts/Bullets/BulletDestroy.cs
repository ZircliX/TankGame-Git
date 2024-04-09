using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 13)
        {
            Destroy(other.gameObject);
        }
    }
}
