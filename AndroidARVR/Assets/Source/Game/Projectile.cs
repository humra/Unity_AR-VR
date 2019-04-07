using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float forceMultiplier = 100f;

    Rigidbody rb;
    float beginTime;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * forceMultiplier, ForceMode.Impulse);
        beginTime = Time.time;
    }

    void Update()
    {
        if (rb.velocity.magnitude < 0.1f && (Time.time - beginTime) > 2f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("ProjectileDestroyer") || collision.gameObject.tag.Equals("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
