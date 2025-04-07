using System.Linq;
using UnityEngine;

public class BaseBallisticWeaponMod : MonoBehaviour, IWeaponModifier
{
    public void OnShoot(GameObject bullet, Weapon weapon)
    {
        BaseAttribute baseAttribute = null;
        BallisticWeaponAttributes ballisticWeapon = null;

        if ((baseAttribute = weapon.WeaponAttributes.OfType<BaseAttribute>().FirstOrDefault()) != null &&
            (ballisticWeapon = weapon.WeaponAttributes.OfType<BallisticWeaponAttributes>().FirstOrDefault()) != null)
        {
            var projectile = bullet.AddComponent<BaseProjectile>();
            projectile.Direction = weapon.firePoint.transform.up;
            projectile.MaxRange = baseAttribute.Range;
            projectile.Speed = baseAttribute.Speed;
        }
    }
}
