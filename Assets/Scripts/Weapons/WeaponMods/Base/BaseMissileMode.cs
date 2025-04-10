using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseMissileMode : WeaponModifier
{
    public override bool OnShoot(GameObject bullet, List<IAttribute> weaponAttributes, Transform firePoint)
    {
        if (bullet != null)
        {
            BaseAttribute baseAttribute = null;
            BaseMissileAttribute missileAttribute = null;
            if ((baseAttribute = weaponAttributes.OfType<BaseAttribute>().FirstOrDefault()) != null &&
                (missileAttribute = weaponAttributes.OfType<BaseMissileAttribute>().FirstOrDefault()) != null)
            {
                var projectile = bullet.AddComponent<BaseMissile>();

                projectile.Direction = Quaternion.AngleAxis(
                    UnityEngine.Random.Range(-missileAttribute.Spread / 2f, missileAttribute.Spread / 2f),
                    Vector3.forward) * firePoint.transform.up;
                projectile.FuelPerSecond = missileAttribute.FuelPerSecond;
                projectile.Fuel = missileAttribute.Fuel;
                projectile.Acceleration = missileAttribute.MissileAcceleration;
                projectile.MaxSpeed = missileAttribute.MissileMaxSpeed;
                projectile.StartingSpeed = missileAttribute.MissileStartingSpeed;

                var coroutine = WaitForShooting(baseAttribute, new List<IAttribute>() { missileAttribute });
                StartCoroutine(coroutine);
            }
            else
            {
                CanShoot = false;
            }
        }

        return CanShoot;
    }

    protected override IEnumerator WaitForShooting(BaseAttribute baseAttribute, List<IAttribute> additionalAttributes)
    {
        BaseMissileAttribute bm = additionalAttributes[0] as BaseMissileAttribute;

        if(bm.MissilesInPod > 0)
        {
            CanShoot = false;
            yield return new WaitForSeconds(1f / baseAttribute.FireRate);
            CanShoot = true;
            bm.MissilesInPod--;
        }
        else
        {
            if (bm.CanBeReloaded && bm.ReloadAmounts > 0)
            {
                CanShoot = false;
                yield return new WaitForSeconds(bm.ReloadTime);
                CanShoot = true;
                bm.ReloadAmounts--;
                bm.MissilesInPod = bm.MissilesInPodMax;
            }
            else
            {
                CanShoot = false;
            }
        }
    }
}
