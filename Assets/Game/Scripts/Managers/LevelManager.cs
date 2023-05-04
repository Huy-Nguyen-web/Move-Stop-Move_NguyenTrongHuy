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
        Debug.Log("Respawn Enemy");
        numberOfEnemyInQueue--;
        if(numberOfEnemyInQueue <= currentNumberOfEnemy) return;
        
        Enemy enemy = SpawnEnemy();
        enemy.OnInit();
    }
}
