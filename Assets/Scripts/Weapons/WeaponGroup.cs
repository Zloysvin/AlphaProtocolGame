using System.Collections.Generic;
using UnityEngine;

public class WeaponGroup
{
    private List<Weapon> weapons;

    public bool isLinked { get; private set; }

    private int activeWeaponIndex;

    public WeaponGroup(List<Weapon> weapons, bool isLinked)
    {
        this.weapons = weapons;
        this.isLinked = isLinked;

        activeWeaponIndex = -1;
    }

    public List<Weapon> GetActiveWeapons()
    {
        if(isLinked)
            return weapons;

        activeWeaponIndex++;
        if (activeWeaponIndex >= weapons.Count)
            activeWeaponIndex = 0;
        return new List<Weapon> { weapons[activeWeaponIndex] };
        //return isLinked ? weapons : new List<Weapon> { weapons[activeWeaponIndex] };
    }
}
