using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private enum GameState{ Menu, Start, Weapon, Skin, End }
    private Dictionary<GameState, Canvas> stateUI = new Dictionary<GameState, Canvas>();
    private GameState currentState;
    public Canvas mainMenuUI;
    public Canvas weaponShopUI;
    public Canvas skinShopUI;
    public Canvas inGameUI;
    public Canvas endGameUI;
    private void Start() {
        stateUI.Add(GameState.Menu, mainMenuUI);
        stateUI.Add(GameState.Start, inGameUI);
        stateUI.Add(GameState.Weapon, weaponShopUI);
        stateUI.Add(GameState.Skin, skinShopUI);
        stateUI.Add(GameState.End, endGameUI);
        currentState = GameState.Menu;
        OpenUI(stateUI, currentState);
    }
    public void GameStart(){
        currentState = GameState.Start;
        OpenUI(stateUI, currentState);
    }
    public void GameEnd(){
        currentState = GameState.End;
        OpenUI(stateUI, currentState);
    }
    public void OpenShop(){
        currentState = GameState.Weapon;
        OpenUI(stateUI, currentState);
    }
    public void MainMenu(){
        currentState = GameState.Menu;
        OpenUI(stateUI, currentState);
    }
    private void OpenUI(Dictionary<GameState, Canvas> stateUI, GameState currentState){
        foreach(var gameState in stateUI.Keys){
            if(gameState == currentState){
                stateUI[gameState].enabled = true;
            }else{
                stateUI[gameState].enabled = false;
            }
        }
    }
}
