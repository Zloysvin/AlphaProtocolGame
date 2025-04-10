using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(ShipController))]
public class PlayerController : MonoBehaviour
{
    public PlayerActions_Asset PlayerActions;

    public ShipController Controller;

    private InputAction move;
    private InputAction look;
    private InputAction fire;

    private bool isShooting = false;
    private bool shootReleased = true;

    private void OnEnable()
    {
        move = PlayerActions.Player.Move;
        move.Enable();

        look = PlayerActions.Player.Look;
        look.Enable();

        fire = PlayerActions.Player.Attack;
        fire.Enable();
        fire.performed += StartShooting;
        fire.canceled += StopShooting;
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

        if (isShooting)
        {
            Controller.Shoot(shootReleased);
            shootReleased = false;
        }
    }

    private void StartShooting(InputAction.CallbackContext context)
    {
        isShooting = true;
    }

    private void StopShooting(InputAction.CallbackContext context)
    {
        isShooting = false;
        shootReleased = true;
    }

    private void FixedUpdate()
    {
        Controller.Accelerate(move.ReadValue<Vector2>());

    }
}
