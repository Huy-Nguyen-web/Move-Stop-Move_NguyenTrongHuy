using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState {Menu, Weapon, Skin, Start, End};
    public GameState currentState;
    private void Start() {
        currentState = GameState.Menu;
    }
    public void StartGame(){
        currentState = GameState.Start;
        UIManager.Instance.GameStart();
    }
    public void EndGame(){
        currentState = GameState.End;
        UIManager.Instance.GameStart();
    }
}
