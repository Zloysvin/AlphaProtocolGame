using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float SmoothFactor1 = 0.3f;
    public float SmoothFactor2 = 0.3f;
    private InputAction look;
    private PlayerActions_Asset PlayerActions;

    private Camera _camera;

    void Awake()
    {
        _camera = GetComponent<Camera>();
        PlayerActions = new PlayerActions_Asset();
    }

    private void OnEnable()
    { look = PlayerActions.Player.Look;
        look.Enable();
    }

    private void OnDisable()
    {
        look.Disable();
    }

    void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector2 lookVector2 = look.ReadValue<Vector2>();
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

        if (lookVector2 is not { x: 0, y: 0 })
            mousePos = (Vector2)Target.position + lookVector2 * 5f;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, mousePos.x, Time.deltaTime * SmoothFactor1),
            Mathf.Lerp(transform.position.y, mousePos.y, Time.deltaTime * SmoothFactor1),
            transform.position.z);

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, Target.position.x, Time.fixedDeltaTime * SmoothFactor1),
            Mathf.Lerp(transform.position.y, Target.position.y, Time.fixedDeltaTime * SmoothFactor2),
            transform.position.z);
    }
}
