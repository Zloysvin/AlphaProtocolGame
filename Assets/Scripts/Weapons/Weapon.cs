using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Dictionary<WeaponModifier, bool> WeaponModifiers;
    public List<IAttribute> WeaponAttributes;

    public void Awake()
    {
        var modifiers = GetComponents<WeaponModifier>().ToList();

        WeaponModifiers = new Dictionary<WeaponModifier, bool>();

        foreach (var modifier in modifiers)
        {
            WeaponModifiers.Add(modifier, true);
        }

        WeaponAttributes = GetComponents<IAttribute>().ToList();
    }

    public void Shoot()
    {
        bool canShoot = WeaponModifiers.Values.All(v => v);
        GameObject bullet = null;

        if (canShoot)
        {
            bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }

        foreach (var mod in WeaponModifiers)
        {
            WeaponModifiers[mod.Key] = mod.Key.OnShoot(bullet, WeaponAttributes, firePoint);
        }

    }
}