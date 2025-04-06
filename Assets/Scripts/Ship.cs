using UnityEngine;

[RequireComponent(typeof(ShipController))]
public class Ship : MonoBehaviour
{
    [Header("Ship propulsion")] 
    public float MaxSpeed = 4f;
    public float ForwardSpeed = 300f;
    public float BackwardSpeed = 150f;
    public float StrafeSpeed = 300f;
    [Space(10f)] 
    public float RotationSpeed = 100f;

    [HideInInspector] 
    public ShipController Controller;

    public void Awake()
    {
        Controller = GetComponent<ShipController>();
    }

    public void Start()
    {
        Controller.SetParameters(MaxSpeed, ForwardSpeed, BackwardSpeed, StrafeSpeed, RotationSpeed);
    }
}
