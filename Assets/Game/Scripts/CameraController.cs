using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject player;
    [SerializeField] private float movingSpeed;
    private void Update() {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, movingSpeed * Time.deltaTime);
    }
}
