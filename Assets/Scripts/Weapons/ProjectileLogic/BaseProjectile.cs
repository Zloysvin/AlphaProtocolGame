using UnityEngine;
using UnityEngine.EventSystems;

public class BaseProjectile : MonoBehaviour
{
    private Vector3 lastPosition;
    private float totalDistanceTraveled;

    public float Speed { set; private get; }
    public float MaxRange { set; private get; }

    public Vector3 Direction { set; private get; }

    void Start()
    {
        lastPosition = transform.position;
        totalDistanceTraveled = 0f;
    }

    void Update()
    {
        transform.position += Direction * Speed * Time.deltaTime;

        float distanceThisFrame = Vector3.Distance(transform.position, lastPosition);
        totalDistanceTraveled += distanceThisFrame;
        lastPosition = transform.position;

        if (totalDistanceTraveled >= MaxRange)
        {
            Destroy(gameObject);
        }
    }
}
