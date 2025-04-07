using System;
using UnityEngine;

public interface IWeaponModifier
{
    public void OnShoot(GameObject bullet, Weapon weapon);
}
