using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCircle : MonoBehaviour
{
    private LineRenderer lineRenderer;
    
    private void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.color = Color.gray;
    }
    public void UpdateCircle(float radius){
        lineRenderer.positionCount = 33;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;

        float angleStep = 360f/32f;

        for (int i = 0; i <= 32; i++){
            float angle = i * angleStep;
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            Vector3 pos = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, pos);
        }
    }
    
}
