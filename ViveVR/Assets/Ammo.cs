using UnityEngine;

public class Ammo : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
