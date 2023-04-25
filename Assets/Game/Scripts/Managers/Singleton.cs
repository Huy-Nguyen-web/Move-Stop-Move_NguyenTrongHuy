using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;
    protected virtual void Awake() {
        if( instance != null && instance != this){
            Destroy(this);
        }else{
            DontDestroyOnLoad(this);
            instance = (T) FindObjectOfType(typeof(T));
        }
    }
    public static T Instance{
        get{
            if(instance == null){
                instance = (T) FindObjectOfType(typeof(T));
            }
            return instance;
        }
    }
}
