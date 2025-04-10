using UnityEngine;

public class BaseMissile : MonoBehaviour
{
    private float speed;
    private float currentSpeed;
    public float MaxSpeed { set; private get; }
    public float StartingSpeed { set; private get; }
    public float Acceleration { set; private get; }
    public float Fuel { set; private get; }
    public float FuelPerSecond { set; private get; }

    public Vector3 Direction { set; private get; }

    void Start()
    {
        currentSpeed = StartingSpeed;
    }

    void Update()
    {
        transform.position += Direction * currentSpeed * Time.deltaTime;

        Fuel -= FuelPerSecond * Time.deltaTime;

        if (Fuel <= 0)
        {
            Destroy(gameObject);
        }

        currentSpeed += Acceleration * Time.deltaTime;
        currentSpeed = Mathf.Min(currentSpeed, MaxSpeed);
    }
}
