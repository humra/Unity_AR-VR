using UnityEngine;

public class Target : MonoBehaviour
{
    Vector3 startingPosition;
    
    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if(Mathf.Abs(Vector3.Distance(startingPosition, transform.position)) > 2)
        {
            FindObjectOfType<Game>().TargetDestroyed();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Floor"))
        {
            FindObjectOfType<Game>().TargetDestroyed();
            Destroy(gameObject);
        }
    }
}
