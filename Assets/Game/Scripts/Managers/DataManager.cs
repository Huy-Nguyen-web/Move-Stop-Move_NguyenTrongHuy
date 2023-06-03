using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager> 
{
    public string playerName;
    public int playerWeaponIndex;
    public int playerCoins;
    private void Start() {
        if(!PlayerPrefs.HasKey("playerName")){
            SetName("You");
        }
        playerName = GetName();
        if(!PlayerPrefs.HasKey("playerWeaponIndex")){
            SetWeaponIndex(0);
        }
        playerWeaponIndex = GetWeaponIndex();
        if(!PlayerPrefs.HasKey("playerCoins")){
            SetCoin(0);
        }
        playerCoins = GetCoin();
    }
    public void SetName(string playerName){
        PlayerPrefs.SetString("playerName", playerName);
    }
    public void SetWeaponIndex(int weaponIndex){
        PlayerPrefs.SetInt("playerWeaponIndex", weaponIndex);
    }
    public void SetCoin(int coins){
        PlayerPrefs.SetInt("playerCoins", coins);
    }
    public string GetName(){
        return PlayerPrefs.GetString("playerName");
    }
    public int GetWeaponIndex(){
        return PlayerPrefs.GetInt("playerWeaponIndex");
    }
    public int GetCoin(){
        return PlayerPrefs.GetInt("playerCoins");
    }
}
