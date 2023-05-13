using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum GameState {Menu, Weapon, Skin, Start, End};
    public GameState currentState;
    private void Start() {
        currentState = GameState.Menu;
    }
    public void GameStart(){
        currentState = GameState.Start;
        UIManager.Instance.GameStart();
    }
    public void GameEnd(){
        currentState = GameState.End;
        UIManager.Instance.GameEnd();
    }
    public void GameReset(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
