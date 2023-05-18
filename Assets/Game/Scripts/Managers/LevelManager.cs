using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Weapon weaponPrefab;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private int maxNumberOfEnemy;
    [SerializeField] private int currentNumberOfEnemy;
    private int numberOfEnemyInQueue;
    public void Start(){
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
    public void RespawnEnemy(){
        numberOfEnemyInQueue--;
        if(numberOfEnemyInQueue < currentNumberOfEnemy) return;
        
        Enemy enemy = SpawnEnemy();
        enemy.OnInit();
    }


    public void LoadLevel(int level){

    }

    public void OnInit(){

    }

    public void OnReset(){

    }

    public Vector3 GetRandomPoint(){
        return Vector3.zero;
    }

}
