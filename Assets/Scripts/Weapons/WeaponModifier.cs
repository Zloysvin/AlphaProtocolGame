using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponModifier : MonoBehaviour
{
    protected bool CanShoot = true;

    public abstract bool OnShoot(GameObject bullet, List<IAttribute> weaponAttributes, Transform firePoint);

    protected abstract IEnumerator WaitForShooting(BaseAttribute baseAttribute, List<IAttribute> additionalAttributes);
}
