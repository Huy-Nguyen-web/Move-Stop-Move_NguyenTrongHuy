using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponShopUI : MonoBehaviour
{
    private int currentWeaponIndex;
    private int currentSelectedWeapon;
    private int maxWeaponAmount;
    private List<GameObject> weaponModels = new List<GameObject>();
    private int weaponModelRenderLayer;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponDescription;
    [SerializeField] private Transform weaponContainer;
    [SerializeField] private Button selectButton;
    [SerializeField] private TextMeshProUGUI selectButtonText;
    private void Start() {
        weaponModelRenderLayer = LayerMask.NameToLayer("Water");
        Debug.Log(DataManager.Instance.playerWeaponIndex);
        currentWeaponIndex = DataManager.Instance.playerWeaponIndex;
        currentSelectedWeapon = DataManager.Instance.playerWeaponIndex;
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
        if(DataManager.Instance.CheckHasBoughtItem(CosmeticManager.Instance.weapons[currentWeaponIndex].weaponName)){
            if(currentWeaponIndex == currentSelectedWeapon){
                selectButton.interactable = false;
                selectButtonText.text = "Selected";
            }else{
                selectButton.interactable = true;
                selectButtonText.text = "Select";
            }
        }else{
            selectButtonText.text = CosmeticManager.Instance.weapons[currentWeaponIndex].weaponPrice.ToString();
            selectButton.interactable = true;
        }
        for(int i = 0; i <= maxWeaponAmount; i++){
            if(currentWeaponIndex == i) {
                weaponModels[i].SetActive(true);
            }else{
                weaponModels[i].SetActive(false);
            }
        }
    }
    
    public void ChangeCurrentWeapon(){
        if(DataManager.Instance.CheckHasBoughtItem(CosmeticManager.Instance.weapons[currentWeaponIndex].weaponName)){
            currentSelectedWeapon = currentWeaponIndex;
            CosmeticManager.Instance.ChangeCurrentWeapon(currentWeaponIndex);
        }else{
            if(DataManager.Instance.playerCoins >= CosmeticManager.Instance.weapons[currentWeaponIndex].weaponPrice){
                DataManager.Instance.SetBoughtItem(CosmeticManager.Instance.weapons[currentWeaponIndex].weaponName, true);
                DataManager.Instance.SubtractCoin(CosmeticManager.Instance.weapons[currentWeaponIndex].weaponPrice);

                UIManager.Instance.UpdateCoinAmount(DataManager.Instance.GetCoin());
                currentSelectedWeapon = currentWeaponIndex;
                CosmeticManager.Instance.ChangeCurrentWeapon(currentWeaponIndex);
            }
        }
        UpdateWeaponUI();
    }
}
