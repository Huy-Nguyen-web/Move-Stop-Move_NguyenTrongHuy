using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCircle : MonoBehaviour
{
    [SerializeField] private Transform areaCircleImage;
    public void UpdateCircle(float radius){
        float offset = 0.8f;
        areaCircleImage.localScale = new Vector3(radius, 1f, radius) * offset;
    }
}
