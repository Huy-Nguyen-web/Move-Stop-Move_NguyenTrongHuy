using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CosmeticManager : Singleton<CosmeticManager>
{
    public Material[] skinColor;
    public SkinData[] pantColor;
    public SkinData[] hats;
    public WeaponData[] weapons;
    [HideInInspector] public WeaponData currentWeapon;
    [HideInInspector] public SkinData currentPant;
    [HideInInspector] public SkinData currentHat;
    public UnityAction<WeaponData> onWeaponChange;
    public UnityAction<SkinData> onPantChange;
    public UnityAction<SkinData> onHatChange;
    public UnityAction<int> onSelectedPantChange;
    public UnityAction<int> onSelectedHatChange;
    private int currentWeaponIndex;
    private void Awake() {
        if(!PlayerPrefs.HasKey("currentWeapon")){
            PlayerPrefs.SetInt("currentWeapon", 0);
        }
        currentWeaponIndex = PlayerPrefs.GetInt("currentWeapon");
        currentWeapon = weapons[currentWeaponIndex];
    }
    public void ChangeCurrentWeapon(int weaponIndex){
        currentWeapon = weapons[weaponIndex];
        PlayerPrefs.SetInt("currentWeapon", weaponIndex);
        onWeaponChange?.Invoke(currentWeapon);
    }
    public void ChangeCurrentPant(int pantIndex){
        currentPant = pantColor[pantIndex];
        onPantChange?.Invoke(currentPant);
    }
    public void ChangeCurrentHat(int hatIndex){
        currentHat = hats[hatIndex];
        onHatChange?.Invoke(currentHat);
    }
    public void ChangeSelectedPant(int pantIndex){
        onSelectedPantChange?.Invoke(pantIndex);
    }
    public void ChangeSelectedHat(int hatIndex){
        onSelectedHatChange?.Invoke(hatIndex);
    }
    public void SetCoin(int coinAmount){
        PlayerPrefs.SetInt("Coin", coinAmount);
    }
    public int GetCoin(){
        int coinAmount = PlayerPrefs.GetInt("Coin");
        return coinAmount;
    }
}
