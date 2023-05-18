using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Player player;
    [SerializeField] private float movingSpeed;
    private void Update() {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset + new Vector3(0, 2, -1) * (player.weaponType.weaponExtraRange + player.characterSize), movingSpeed * Time.deltaTime);
    }
}
