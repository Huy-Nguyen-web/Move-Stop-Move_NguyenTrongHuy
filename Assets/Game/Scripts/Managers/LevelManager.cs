using System.Collections;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Weapon weaponPrefab;
    [SerializeField] private WaypointUI waypointPrefab;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private int maxNumberOfEnemy;
    [SerializeField] private int currentNumberOfEnemy;
    [SerializeField] private string[] characterRandomNames;
    [SerializeField] private Present present;
    private int numberOfEnemyInQueue;
    public void Awake(){
        // numberOfEnemyInQueue = maxNumberOfEnemy;
        // for(int i = 0; i < currentNumberOfEnemy; i++){
        //     Enemy enemy = SpawnEnemy();
        //     enemy.OnInit();
        // }
        OnInit();
    }
    public void OnInit(){
        numberOfEnemyInQueue = maxNumberOfEnemy;
        for(int i = 0; i < currentNumberOfEnemy; i++){
            Enemy enemy = SpawnEnemy();
            enemy.OnInit();
        }
    }
    public Weapon SpawnWeapon(){
        Weapon weapon = SimplePool.Spawn<Weapon>(weaponPrefab);
        return weapon;
    }
    public Enemy SpawnEnemy(){
        Enemy enemy = SimplePool.Spawn<Enemy>(enemyPrefab);
        return enemy;
    }
    public WaypointUI SpawnWaypoint(){
        WaypointUI waypoint = SimplePool.Spawn<WaypointUI>(waypointPrefab);
        return waypoint;
    }
    public void RespawnEnemy(){
        numberOfEnemyInQueue--;
        if(numberOfEnemyInQueue < currentNumberOfEnemy) return;
        Enemy enemy = SpawnEnemy();
        enemy.OnInit();
    }
    // public void LoadLevel(int level){

    // }
    // public void OnInit(){

    // }
    public string GetRandomName(){
        string characterRandomName = characterRandomNames[Random.Range(0, characterRandomNames.Length - 1)]; 
        return characterRandomName;
    }
    // public void OnReset(){

    // }
    public Vector3 GetRandomPoint(){
        return Vector3.zero;
    }
    
    private IEnumerator SpawnPresent(){
        yield return new WaitForSeconds(5f);
        present.gameObject.SetActive(true);
    }
    public void ResetPresent(){
        present.gameObject.SetActive(false);
        StartCoroutine(SpawnPresent());
    }
}
