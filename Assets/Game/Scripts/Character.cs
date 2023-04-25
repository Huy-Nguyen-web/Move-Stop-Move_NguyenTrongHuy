using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector3 moveDirection;
    public Transform currentTarget;
    public GameObject hammerPrefab;
    public List<Transform> enemyInRange;
    // public Collider hitArea;
    public Animator animator;
    public LayerMask characterLayer;
    public float hitRange = 10f;
    public bool isThrowing = false;

    // public void UpdateHitArea(){
    //     hitArea.transform.localScale = new Vector3(hitRange, hitRange, hitRange);
    // }
    // public void UpdateCurrentTarget(){
    //     if(enemyInRange.Length > 0){
    //         currentTarget = enemyInRange[0];
    //     }else{
    //         currentTarget = null;
    //     }
    // }
    public virtual void Attack(){
        transform.LookAt(currentTarget.transform.position);
        if(!isThrowing){
            isThrowing = true;
            // TODO: Rewrite the GameObject to Hammer
            GameObject hammer = Instantiate(hammerPrefab, transform.position + transform.forward, transform.rotation);
            
            Hammer hammerScript = hammer.GetComponent<Hammer>();
            hammerScript.currentCharacter = this.gameObject;
            hammerScript.maxTravelDistance = hitRange/2;
        }
    }
    public void UpdateEnemyList(){
        Collider[] foundCharacter = Physics.OverlapSphere(transform.position, hitRange/2, characterLayer);
        enemyInRange.Clear();
        for(int i = 0; i < foundCharacter.Length; i++){
           if(foundCharacter[i].transform == transform) continue;
           Transform enemy = foundCharacter[i].transform;
           enemyInRange.Add(enemy);
        }
    }
}
