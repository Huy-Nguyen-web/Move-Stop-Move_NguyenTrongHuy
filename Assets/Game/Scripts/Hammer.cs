using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private float speed = 10.0f;
    [SerializeField] private Rigidbody rigidbody;
    private Vector3 startPosition;
    public float maxTravelDistance;
    public GameObject currentCharacter;

    private void Start() {
        rigidbody.velocity = transform.forward * speed;
        startPosition = transform.position;
    }
    private void Update() {
        if(Vector3.Distance(startPosition, transform.position) >= maxTravelDistance){
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == currentCharacter) return;
        // After killing enemy, going for other enemy
        Character characterScript = currentCharacter.GetComponent<Character>();
        // characterScript.currentTarget = null;
        // characterScript.enemyInRange.Remove(other.gameObject);

        this.gameObject.SetActive(false);
        other.gameObject.SetActive(false);
    }
}
