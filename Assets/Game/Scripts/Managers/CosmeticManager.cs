using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CosmeticManager : Singleton<CosmeticManager>
{
    public WeaponData[] weapons;
    public WeaponData currentWeapon;
    public UnityAction<WeaponData> onWeaponChange;
    private void Awake() {
        currentWeapon = weapons[0];
    }
    public void ChangeCurrentWeapon(int weaponIndex){
        currentWeapon = weapons[weaponIndex];
        onWeaponChange?.Invoke(currentWeapon);
    }
}
