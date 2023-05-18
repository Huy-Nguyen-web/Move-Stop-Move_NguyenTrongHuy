using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable Object Manager", menuName = "SOManager")]
public class SOManager : ScriptableObject
{
    public SkinData[] pantScriptableObjectManager;
    public SkinData[] hatScriptableObjectManager;
    public WeaponData[] weaponScriptableObjectManager;
}
