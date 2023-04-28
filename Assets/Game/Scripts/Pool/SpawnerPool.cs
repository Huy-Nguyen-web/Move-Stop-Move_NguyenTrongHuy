using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerPool : MonoBehaviour 
{
    [SerializeField] private Transform _root;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _amount;
    private ObjectPool<GameObject> _pool;

    private void Start() {
        OnInit();
    }
    private void OnInit(){
        _pool = new ObjectPool<GameObject>(() => {
            return Instantiate(_prefab, _root);
        }, prefab => {
            prefab.gameObject.SetActive(true);
        }, prefab => {
            prefab.gameObject.SetActive(false);
        }, prefab => {
            Destroy(prefab.gameObject);
        }, true, 25, 50);

        for(int i = 0; i < _amount; i++){
            SpawnPrefab();
        }
    }
    public GameObject SpawnPrefab(){
        GameObject prefab = _pool.Get();
        return prefab;
    }
    public void KillPrefab(GameObject prefab){
        _pool.Release(prefab);
    }
    // [SerializeField] private Enemy enemyPrefab;
    // [SerializeField] private Weapon weaponPrefab;
    // public void SpawnEnemy() {
    //     Enemy enemy = SimplePool.Spawn<Enemy>(enemyPrefab);
    //     Weapon weapon = SimplePool.Spawn<Weapon>(weaponPrefab);
    // }
}
