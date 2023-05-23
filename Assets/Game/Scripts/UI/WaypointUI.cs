using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointUI : GameUnit
{
    public Image image;
    public Transform target;
    public void OnInit(Character character){
        target = character.transform;
        Debug.Log("waypoint init");
    }
    private void Update() {
        float minX = image.GetPixelAdjustedRect().width/2;
        float maxX = Screen.width - minX;

        float minY = image.GetPixelAdjustedRect().height/2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        float offsetY = 100;
        float newPosY = pos.y + offsetY;

        newPosY = Mathf.Clamp(newPosY, minY, maxY);

        image.transform.position = new Vector2(pos.x, newPosY);
    }
    public override void OnInit()
    {
        throw new System.NotImplementedException();
    }
    public override void OnDespawn()
    {
        throw new System.NotImplementedException();
    }
}
