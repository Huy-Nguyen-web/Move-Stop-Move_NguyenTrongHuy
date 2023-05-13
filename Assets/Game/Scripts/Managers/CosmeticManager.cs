using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CosmeticManager : Singleton<CosmeticManager>
{
    public Material[] skinColor;
    public Material[] pantColor;
    public WeaponData[] weapons;
    public WeaponData currentWeapon;
    public Material currentPant;
    public UnityAction<WeaponData> onWeaponChange;
    public UnityAction<Material> onPantChange;
    private void Awake() {
        currentWeapon = weapons[0];
    }
    public void ChangeCurrentWeapon(int weaponIndex){
        currentWeapon = weapons[weaponIndex];
        onWeaponChange?.Invoke(currentWeapon);
    }
    public void ChangeCurrentPant(int pantIndex){
        currentPant = pantColor[pantIndex];
        onPantChange?.Invoke(currentPant);
    }
}
