using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Dictionary<WeaponModifier, bool> WeaponModifiers;
    public List<IAttribute> WeaponAttributes;
    public ParticleSystem VFX;

    private AudioSource audio;

    public void Awake()
    {
        var modifiers = GetComponents<WeaponModifier>().ToList();

        WeaponModifiers = new Dictionary<WeaponModifier, bool>();

        foreach (var modifier in modifiers)
        {
            WeaponModifiers.Add(modifier, true);
        }

        WeaponAttributes = GetComponents<IAttribute>().ToList();

        audio = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        bool canShoot = WeaponModifiers.Values.All(v => v);
        GameObject bullet = null;

        if (canShoot)
        {
            bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            VFX.Emit(1);
            audio.PlayOneShot(audio.clip);
        }

        for (int i = 0; i < WeaponModifiers.Count; i++)
        {
            var key = WeaponModifiers.ElementAt(i).Key;
            WeaponModifiers[key] = key.OnShoot(bullet, WeaponAttributes, firePoint);
        }
    }
}