using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform weaponHolder;
    public Weapon startingWeapon;
    Weapon equippedWeapon;

    void Start()
    {
        if (startingWeapon != null)
            EquipWeapon(startingWeapon);
    }

    public void EquipWeapon(Weapon weaponToEquip)
    {
        if (equippedWeapon != null)
            Destroy(equippedWeapon.gameObject);

        equippedWeapon = Instantiate(weaponToEquip, weaponHolder.position, weaponHolder.rotation) as Weapon;
        equippedWeapon.transform.parent = weaponHolder;
    }

    public void Shoot()
    {
        if(equippedWeapon != null)
        {
            equippedWeapon.Shoot();
        }
    }
   
}
