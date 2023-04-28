using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _amount;
    private ObjectPool<GameObject> _pool;

    private void Start() {
        _pool = new ObjectPool<GameObject>( () => {
            return Instantiate(_prefab, _root);
        }, prefab => {
            prefab.gameObject.SetActive(true);
        }, prefab => {
            prefab.gameObject.SetActive(false);
        }, prefab => {
            Destroy(prefab.gameObject);
        }, false, 25, 50);

        OnInit();
    }
    private void OnInit(){
        for(int i = 0; i < _amount; i++){
            GameObject prefab = _pool.Get();
        }
    }
    private void KillPrefab(GameObject prefab){
        _pool.Release(prefab);
    }
}
