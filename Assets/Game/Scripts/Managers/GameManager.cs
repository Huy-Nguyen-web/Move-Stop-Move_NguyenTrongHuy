using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    
    public enum GameState {Menu, Weapon, Skin, Start, End};
    private GameState currentState;
    private void Start() {
        currentState = GameState.Menu;
    }
    public void GameStart(){
        currentState = GameState.Start;
        UIManager.Instance.GameStart();
    }
    public void GameEnd(string gameEndMessage){
        currentState = GameState.End;
        UIManager.Instance.GameEnd(gameEndMessage);
    }
    public void GameReset(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static void ChangeState(GameState gameState) => GameManager.Instance.currentState = gameState;
    public static bool IsState(GameState gameState) => GameManager.Instance.currentState == gameState;
}
