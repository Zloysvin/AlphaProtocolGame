using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ShipController))]
public class PlayerController : MonoBehaviour
{
    public PlayerActions_Asset PlayerActions;

    public ShipController Controller;

    private InputAction move;
    private InputAction look;
    private InputAction fire;

    private void OnEnable()
    {
        move = PlayerActions.Player.Move;
        move.Enable();

        look = PlayerActions.Player.Look;
        look.Enable();

        fire = PlayerActions.Player.Attack;
        fire.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
        fire.Disable();
    }

    private void Awake()
    {
        PlayerActions = new PlayerActions_Asset();

        Controller = GetComponent<ShipController>();
    }

    private void Update()
    {
        Vector2 lookVector2 = look.ReadValue<Vector2>();

        if (lookVector2 is { x: 0, y: 0 })
        {
            Controller.Rotate(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else
        {
            Controller.Rotate((Vector2)transform.position + lookVector2);
        }
    }

    private void FixedUpdate()
    {
        Controller.Accelerate(move.ReadValue<Vector2>());

    }
}
