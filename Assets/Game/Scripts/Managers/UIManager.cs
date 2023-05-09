using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private enum GameState{ Menu, Start, Weapon, Skin, End }
    private GameState currentState;
    public Canvas mainMenuUI;
    public Canvas weaponShopUI;
    public Canvas inGameUI;
    public Canvas endGameUI;
    private void Start() {
        MainMenu();
    }
    public void GameStart(){
        currentState = GameState.Start;
        mainMenuUI.enabled = false;
        weaponShopUI.enabled = false;
        inGameUI.enabled = true;
    }
    public void EndGame(){
        currentState = GameState.End;
        mainMenuUI.enabled = false;
        weaponShopUI.enabled = false;
        inGameUI.enabled = false;
    }
    public void OpenShop(){
        currentState = GameState.Weapon;
        mainMenuUI.enabled = false;
        weaponShopUI.enabled = true;
        inGameUI.enabled = false;
    }
    public void MainMenu(){
        currentState = GameState.Menu;
        mainMenuUI.enabled = true;
        weaponShopUI.enabled = false;
        inGameUI.enabled = false;
    }
}
