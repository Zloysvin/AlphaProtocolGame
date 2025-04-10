using System.Collections.Generic;
using System.Linq;
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

    [Space(20f)]
    [Header("Energy")]
    public float CurrentEnergy = 0f;
    public float MaxEnergy = 100f;
    public float EnergyRegenRate = 10f;

    [Space(20f)]
    [Header("Weapons")]
    public List<Weapon> Weapons = new List<Weapon>();
    public List<WeaponGroup> WeaponGroups = new List<WeaponGroup>();
    public int ActiveWeaponGroup = 0;

    [HideInInspector] 
    public ShipController Controller;

    public void Awake()
    {
        Controller = GetComponent<ShipController>();
        Weapons = GetComponentsInChildren<Weapon>().ToList();

        WeaponGroups.Add(new WeaponGroup(Weapons, true));
    }

    public void Start()
    {
        Controller.SetParameters(MaxSpeed, ForwardSpeed, BackwardSpeed, StrafeSpeed, RotationSpeed);
    }

    public void Update()
    {
        if (CurrentEnergy < MaxEnergy)
        {
            CurrentEnergy += EnergyRegenRate * Time.deltaTime;

            if (CurrentEnergy > MaxEnergy)
            {
                CurrentEnergy = MaxEnergy;
            }
        }
    }
}
