using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skin", menuName = "Skin")]
public class SkinData : ScriptableObject
{
    [HideInInspector] public enum EquimentType{ Pant, Hat }
    public string skinName;
    public string skinDescription;
    public Material skinMaterial;
    public GameObject HatModel;
    public EquimentType equimentType;
    public Sprite skinIcon;
    public int skinPrice;
}
