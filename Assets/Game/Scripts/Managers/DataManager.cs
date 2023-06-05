using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager> 
{
    public string playerName;
    public int playerWeaponIndex;
    public int playerHatIndex;
    public int playerPantIndex;
    public int playerCoins;

    private void Start() {
        for(int i = 0; i < CosmeticManager.instance.weapons.Length;i++){
            var weaponName = CosmeticManager.instance.weapons[i].weaponName;
            if(!PlayerPrefs.HasKey(weaponName)){
                SetBoughtItem(weaponName, false);
            }
        }
        if(!PlayerPrefs.HasKey("playerName")){
            SetName("You");
        }
        playerName = GetName();
        if(!PlayerPrefs.HasKey("playerWeaponIndex")){
            SetWeaponIndex(0);
        }
        playerWeaponIndex = GetWeaponIndex();
        if(!PlayerPrefs.HasKey("playerCoins")){
            SetCoin(1000);
        }
        playerCoins = GetCoin();
    }
    public void SetName(string playerName){
        PlayerPrefs.SetString("playerName", playerName);
    }
    public void SetWeaponIndex(int weaponIndex){
        PlayerPrefs.SetInt("playerWeaponIndex", weaponIndex);
    }
    public string GetName(){
        return PlayerPrefs.GetString("playerName");
    }
    public int GetWeaponIndex(){
        return PlayerPrefs.GetInt("playerWeaponIndex");
    }
    public void AddCoin(int coins){
        playerCoins += coins;
        SetCoin(playerCoins);
    }
    public void SubtractCoin(int coins){
        playerCoins -= coins;
        if(playerCoins < 0) playerCoins = 0;
        SetCoin(playerCoins);
    }
    public void SetCoin(int coins){
        PlayerPrefs.SetInt("playerCoins", coins);
    }
    public int GetCoin(){
        return PlayerPrefs.GetInt("playerCoins");
    }
    public bool CheckHasBoughtItem(string itemName){
        if(PlayerPrefs.HasKey(itemName)){
            return PlayerPrefs.GetInt(itemName) == 1;
        }
        return false;
    }
    public void SetBoughtItem(string itemName, bool hasBought){
        PlayerPrefs.SetInt(itemName, hasBought ? 1 : 0);
    }
}
