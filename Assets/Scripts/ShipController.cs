using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour
{
    private Rigidbody2D _rb;

    [Range(0f, 100f)]
    public float rotationSpeed = 25f; //TODO remove it later and put rotationSpeed into Ship

    [Range(0f, 100f)]
    public float limitVelocity = 10f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, _rb.linearVelocity + (Vector2)transform.position, Color.magenta);
    }

    public void Accelerate(Vector2 direction, float power)
    {
        Vector2 worldDirection = transform.TransformDirection(direction.normalized);
        _rb.AddForce(worldDirection * power, ForceMode2D.Impulse);
    }

    public void Rotate(Vector2 target)
    {
        Vector2 directionToTarget = target - (Vector2)transform.position;
        Debug.DrawRay(transform.position, directionToTarget, Color.blue);
        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;
        Debug.Log($"Target: {targetAngle}");

        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
        Debug.Log($"Delta: {newAngle}");

        transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
        Debug.DrawLine(transform.position, transform.position + transform.up * directionToTarget.magnitude, Color.red);
    }

    public void FixedUpdate()
    {
        _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity, limitVelocity);
    }
}
