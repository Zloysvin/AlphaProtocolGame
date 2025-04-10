using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseBallisticWeaponMod : WeaponModifier
{
    public override bool OnShoot(GameObject bullet, List<IAttribute> weaponAttributes, Transform firePoint)
    {
        if(bullet != null)
        {
            BaseAttribute baseAttribute = null;
            BallisticWeaponAttributes ballisticWeapon = null;

            if ((baseAttribute = weaponAttributes.OfType<BaseAttribute>().FirstOrDefault()) != null &&
                (ballisticWeapon = weaponAttributes.OfType<BallisticWeaponAttributes>().FirstOrDefault()) != null)
            {
                var projectile = bullet.AddComponent<BaseProjectile>();


                projectile.Direction = Quaternion.AngleAxis(
                    UnityEngine.Random.Range(-ballisticWeapon.SpreadAngle / 2f, ballisticWeapon.SpreadAngle / 2f),
                    Vector3.forward) * firePoint.transform.up;
                projectile.MaxRange = baseAttribute.Range;
                projectile.Speed = baseAttribute.Speed;

                ballisticWeapon.MagAmount--;

                var coroutine = WaitForShooting(baseAttribute, new List<IAttribute>() { ballisticWeapon });
                StartCoroutine(coroutine);
            }
            else
            {
                CanShoot = false;
            }
        }

        Debug.Log(gameObject.name + " " + CanShoot);
        return CanShoot;
    }

    protected override IEnumerator WaitForShooting(BaseAttribute baseAttribute, List<IAttribute> additionalAttributes)
    {
        BallisticWeaponAttributes bw = additionalAttributes[0] as BallisticWeaponAttributes;

        CanShoot = false;

        if (bw.MagAmount == 0)
        {
            bw.MagAmount = bw.MaxMagAmount;
            Debug.Log("Reloading Weapon!");
            yield return new WaitForSeconds(bw.ReloadTime);
        }
        else
        {
            
            yield return new WaitForSeconds(1f / baseAttribute.FireRate);
            
        }

        CanShoot = true;
    }
}
