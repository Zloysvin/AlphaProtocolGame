using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public IWeaponModifier[] modifiers;
    public IAttribute[] WeaponAttributes;

    public void Awake()
    {
        modifiers = GetComponents<IWeaponModifier>();
        WeaponAttributes = GetComponents<IAttribute>();
    }

    public void Shoot(Ship caller)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        foreach (var mod in modifiers)
        {
            mod.OnShoot(bullet, this);
        }
    }
}