using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaypointUI : GameUnit
{
    public Image image;
    public RectTransform rectSize;
    public Image arrowImage;
    public TMP_Text characterScoreText;
    private Vector3 middleScreenPosition;
    private Character currentCharacter;

    [HideInInspector]
    public Transform target;
    private void Awake() {
        middleScreenPosition = new Vector3(Screen.width/2, Screen.height/2);
    }
    public void OnInit(Character character){
        currentCharacter = character;
        target = currentCharacter.transform;
        currentCharacter.updateCharacterPoint += OnUpdateCharacterPoint;
        image.color = CosmeticManager.Instance.skinColor[character.characterColorIndex].color;
    }
    private void Update() {
        WayPointMovement();
        WayPointRotation();
    }

    private void WayPointRotation(){
        Vector3 imagePosition = image.transform.position;
        Vector2 direction = new Vector2(imagePosition.x - middleScreenPosition.x, imagePosition.y - middleScreenPosition.y);
        float rotationAngle = Vector3.Angle(direction, Vector2.up);
        Vector3 cross = Vector3.Cross(Vector2.up, direction);
        if(cross.z < 0) rotationAngle = -rotationAngle;
        rectSize.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
    }
    private void WayPointMovement(){
        float offset = 5.0f;
        float minX = rectSize.rect.width/2 + offset;
        float maxX = Screen.width - minX;

        float minY = rectSize.rect.height/2 + offset;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position);

        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        float offsetY = 100;
        float newPosY = pos.y + offsetY;

        newPosY = Mathf.Clamp(newPosY, minY, maxY);

        Vector2 dir = pos - new Vector2(Screen.width/2, Screen.height/2);
        if(Mathf.Abs(pos.x - minX) < 0.5f || Mathf.Abs(pos.x - maxX) < 0.5f || Mathf.Abs(pos.y - minY) < 0.5f || Mathf.Abs(pos.y - maxY) < 0.5f){
            arrowImage.enabled = true;
        }else{
            arrowImage.enabled = false;
        }
        
        rectSize.position = new Vector2(pos.x, newPosY);
        characterScoreText.transform.position = rectSize.position;
    }
    public void OnUpdateCharacterPoint(int point){
        characterScoreText.text = point.ToString();
    }
    public override void OnInit()
    {

    }
    public override void OnDespawn()
    {
        currentCharacter.updateCharacterPoint -= OnUpdateCharacterPoint;
    }
}
