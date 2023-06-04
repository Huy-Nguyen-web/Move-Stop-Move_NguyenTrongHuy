using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObstacleCollider : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Material fadeObstacleMaterial;
    [SerializeField] private Player player;
    private Transform currentObstacle;
    private Material currentObstacleMaterial;
    private bool hitObstacle;
    private void OnTriggerEnter(Collider other) {
       
    }
    private void OnTriggerExit(Collider other) {
        
    }
    private void Update() {
        transform.position = player.transform.position;
        // Collider[] obstacles = Physics.OverlapSphere(transform.position, 6f, obstacleLayer);
        // if(obstacles.Length > 0){
        //     for (int i = 0; i < obstacles.Length; i++){
        //         Debug.Log(obstacles[i].transform.GetComponent<MeshRenderer>().material);
        //         obstacles[i].transform.GetComponent<MeshRenderer>().material = fadeObstacleMaterial;
        //     }
        // }
        float offsetY = 100f;
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f - offsetY, 0f);
        Ray ray =  Camera.main.ScreenPointToRay(screenCenter);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, obstacleLayer)){
            if(!hitObstacle){
                hitObstacle = true;
                currentObstacle = hit.transform;
                currentObstacleMaterial = currentObstacle.GetComponent<MeshRenderer>().material;
                currentObstacle.GetComponent<MeshRenderer>().material = fadeObstacleMaterial;
            }
        }else{
            if(currentObstacleMaterial != null){
                currentObstacle.GetComponent<MeshRenderer>().material = currentObstacleMaterial;
                currentObstacle = null;
                currentObstacleMaterial = null;
            }
            hitObstacle = false;
        }
    }
}
