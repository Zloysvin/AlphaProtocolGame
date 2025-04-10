using UnityEngine;

public class BaseMissileAttribute : MonoBehaviour, IAttribute
{
    [Header("Ammo")]
    public int MissilesInPod;
    public int MissilesInPodMax;
    public bool CanBeReloaded;
    public int MaxReloads;
    public int ReloadAmounts;
    public float ReloadTime;
    [Header("Propulsion")]
    public int MissileMaxSpeed;
    public int MissileStartingSpeed;
    public int MissileAcceleration;
    [Header("Fuel")]
    public float Fuel;
    public int FuelPerSecond;
    [Header("Misc")] 
    public float Spread;
}
