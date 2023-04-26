using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private float speed = 10.0f;
    private Vector3 startPosition;
    public float maxTravelDistance;
    public GameObject currentCharacter;

    private void Start() {
        rb.velocity = transform.forward * speed;
        startPosition = transform.position;
    }
    private void Update() {
        if(Vector3.Distance(startPosition, transform.position) >= maxTravelDistance){
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == currentCharacter) return;
        Character characterScript = currentCharacter.GetComponent<Character>();
        this.gameObject.SetActive(false);
        other.gameObject.SetActive(false);
    }
}
