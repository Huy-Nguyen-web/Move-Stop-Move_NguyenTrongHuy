using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public string weaponDescription;
    public GameObject weaponModel;
    public float weaponSpeed;
    public float weaponExtraRange;
    public int weaponPrice;
    public bool canReturn;
    public bool canRotate;

}
