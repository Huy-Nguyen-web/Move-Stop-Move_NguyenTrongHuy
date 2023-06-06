using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    private void OnEnable() {
        SpawnAtRandomPosition();
    }
    public void OnInit(){

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
    private void SpawnAtRandomPosition(){
        Vector3 randomPoint;
        if(RandomPoint(Vector3.zero, 35.0f, out randomPoint)){
            transform.position = randomPoint;
        }
    }
}