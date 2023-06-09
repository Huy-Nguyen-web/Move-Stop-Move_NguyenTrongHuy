using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public Player player;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text announceText;
    [SerializeField] private TMP_Text coinAmountText;
    private void Start() {
        stateUI.Add(GameState.Menu, mainMenuUI);
        stateUI.Add(GameState.Start, inGameUI);
        stateUI.Add(GameState.Weapon, weaponShopUI);
        stateUI.Add(GameState.Skin, skinShopUI);
        stateUI.Add(GameState.End, endGameUI);
        // currentState = GameState.Menu;
        MainMenu();
        OpenUI(stateUI, currentState);
        UpdateCoinAmount(DataManager.Instance.GetCoin());
    }
    public void GameStart(){
        currentState = GameState.Start;
        SoundManager.Instance.PlayMusic(SoundManager.Instance.gameplayMusic);
        OpenUI(stateUI, currentState);
    }
    public void GameEnd(string gameEndMessage){
        currentState = GameState.End;
        announceText.text = gameEndMessage;
        OpenUI(stateUI, currentState);
    }
    public void OpenWeaponShop(){
        currentState = GameState.Weapon;
        OpenUI(stateUI, currentState);
    }
    public void OpenSkinShop(){
        currentState = GameState.Skin;
        OpenUI(stateUI, currentState);
    }
    public void MainMenu(){
        currentState = GameState.Menu;
        SoundManager.Instance.PlayMusic(SoundManager.Instance.mainmenuMusic);
        player.DeleteTempSkin();
        OpenUI(stateUI, currentState);
    }
    public void ChangeCharacterName(){
        player.ChangeCharacterName(inputField.text);
        PlayerPrefs.SetString("playerName", inputField.text);
    }
    public void UpdateCoinAmount(int amount){
        coinAmountText.text = amount.ToString();
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
