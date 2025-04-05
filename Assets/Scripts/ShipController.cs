using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour
{
    private Rigidbody2D _rb;

    [Range(0f, 500f)]
    public float rotationSpeed = 25f; //TODO remove it later and put rotationSpeed into Ship

    [Range(0f, 100f)]
    public float limitVelocity = 10f;

    [Space(20)]

    [Header("Speed")]
    [Range(0f, 1000f)]
    public float ForwardSpeed = 100f;
    [Range(0f, 1000f)]
    public float BackwardsSpeed = 60f;
    [Range(0f, 1000f)]
    public float StrafeSpeed = 90f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, _rb.linearVelocity + (Vector2)transform.position, Color.magenta);
    }

    public void Accelerate(Vector2 direction)
    {
        

        float yDirSpeed = ForwardSpeed;
        if (direction.y < 0f)
            yDirSpeed = BackwardsSpeed;

        Vector2 worldDirection = transform.TransformDirection(direction.normalized);
        _rb.AddForce(new Vector2(worldDirection.x * StrafeSpeed / 10000f, worldDirection.y * yDirSpeed / 10000f), ForceMode2D.Impulse);

        Debug.Log($"Y dir Speed: {yDirSpeed} actual speed: {_rb.linearVelocity.magnitude}");
    }

    public void Rotate(Vector2 target)
    {
        Vector2 directionToTarget = target - (Vector2)transform.position;
        Debug.DrawRay(transform.position, directionToTarget, Color.blue);
        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;

        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle,
            rotationSpeed * Time.deltaTime * (1 - 0.7f * _rb.linearVelocity.magnitude / limitVelocity));

        transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
        Debug.DrawLine(transform.position, transform.position + transform.up * directionToTarget.magnitude, Color.red);
    }

    public void FixedUpdate()
    {
        _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity, limitVelocity);
    }
}
