using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Weapon weaponPrefab;
    [SerializeField] private Enemy enemyPrefab;
    public void Start(){
        for(int i = 0; i < 10; i++){
            SpawnEnemy();
        }
    }
    public Weapon SpawnHammer(){
        Weapon weapon = SimplePool.Spawn<Weapon>(weaponPrefab);
        return weapon;
    }
    public void SpawnEnemy(){
        Enemy enemy = SimplePool.Spawn<Enemy>(enemyPrefab);
    }
}
