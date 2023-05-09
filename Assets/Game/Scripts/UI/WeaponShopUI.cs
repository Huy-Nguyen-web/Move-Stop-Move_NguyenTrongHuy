using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponShopUI : MonoBehaviour
{
    private int currentWeaponIndex;
    private int maxWeaponAmount;
    private List<GameObject> weaponModels = new List<GameObject>();
    private int weaponModelRenderLayer;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponDescription;
    [SerializeField] private Transform weaponContainer;
    private void Start() {
        weaponModelRenderLayer = LayerMask.NameToLayer("Water");
        currentWeaponIndex = 0;
        maxWeaponAmount = CosmeticManager.Instance.weapons.Length - 1;
        for(int i = 0; i <= maxWeaponAmount; i++){
            GameObject weaponModel = Instantiate(CosmeticManager.Instance.weapons[i].weaponModel, weaponContainer);
            weaponModel.transform.localRotation = Quaternion.Euler(0, 0, 160);
            weaponModel.layer = weaponModelRenderLayer;
            weaponModels.Add(weaponModel);
        }
        UpdateWeaponUI();
    }
    public void ChangeWeaponLeft(){
        currentWeaponIndex--;
        if(currentWeaponIndex <= 0) currentWeaponIndex = 0;
        UpdateWeaponUI();
    }
    public void ChangeWeaponRight(){
        currentWeaponIndex++;
        if(currentWeaponIndex >= maxWeaponAmount) currentWeaponIndex = maxWeaponAmount;
        UpdateWeaponUI();
    }
    private void UpdateWeaponUI(){
        weaponName.text = CosmeticManager.Instance.weapons[currentWeaponIndex].weaponName;
        weaponDescription.text = CosmeticManager.Instance.weapons[currentWeaponIndex].weaponDescription;
        for(int i = 0; i <= maxWeaponAmount; i++){
            if(currentWeaponIndex == i) {
                weaponModels[i].SetActive(true);
            }else{
                weaponModels[i].SetActive(false);
            }
        }
    }
}
