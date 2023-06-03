using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCircle : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private Transform areaCircleImage;
    private void Start() {
        // lineRenderer = GetComponent<LineRenderer>();
        // lineRenderer.material.color = Color.gray;
    }
    public void UpdateCircle(float radius){
        // lineRenderer.positionCount = 65;
        // lineRenderer.startWidth = 0.2f;
        // lineRenderer.endWidth = 0.2f;

        // float angleStep = 360f/64f;

        // for (int i = 0; i <= 64; i++){
        //     float angle = i * angleStep;
        //     float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
        //     float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
        //     Vector3 pos = new Vector3(x, y, 0);
        //     lineRenderer.SetPosition(i, pos);
        // }
        float offset = 0.8f;
        areaCircleImage.localScale = new Vector3(radius, radius, radius) * offset;

    }
    
}
