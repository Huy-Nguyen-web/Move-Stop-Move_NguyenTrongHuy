using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinShopUI : MonoBehaviour
{
    private int currentPantIndex;
    private int currentSelectedpant;
    private int pantAmount;
    private int hatAmount;
    private Button button;

    public List<TabButton> tabButtons = new List<TabButton>();
    public List<GameObject> contents = new List<GameObject>();
    [HideInInspector] public TabButton selectedTab;
    [SerializeField] private RectTransform pantButtonContainer;
    [SerializeField] private RectTransform hatButtonContainer;
    [SerializeField] private RectTransform skinButtonContainer;
    [SerializeField] private Button buttonPrefab;

    private void Start() {
        OnTabSelected(tabButtons[1]);
        pantAmount = CosmeticManager.Instance.pantColor.Length - 1;
        hatAmount = CosmeticManager.Instance.hats.Length - 1;
        for(int i = 0; i < pantAmount; i++){
            SpawnPantButton(CosmeticManager.Instance.pantColor[i], i);
        }
        for(int i = 0; i < hatAmount; i++){
            SpawnHatButton(CosmeticManager.Instance.hats[i], i);
        }
    }
    private void SpawnPantButton(SkinData skinData, int index){
        button = Instantiate(buttonPrefab, pantButtonContainer);
        button.onClick.AddListener(delegate{ChangePant(index);}); 
        button.image.sprite = skinData.skinIcon;
    }
    private void SpawnHatButton(SkinData skinData, int index){
        button = Instantiate(buttonPrefab, hatButtonContainer);
        button.onClick.AddListener(delegate{ChangeHat(index);}); 
        button.image.sprite = skinData.skinIcon;
    }
    public void ChangePant(int index){
        CosmeticManager.Instance.ChangeCurrentPant(index);
    }
    public void SelectPant(int index){
        CosmeticManager.Instance.ChangeSelectedPant(index);
    }
    public void ChangeHat(int index){
        CosmeticManager.Instance.ChangeCurrentHat(index);
    }
    public void SelectHat(int index){
        CosmeticManager.Instance.ChangeSelectedHat(index);
    }
    public void Subscribe(TabButton button){
        tabButtons.Add(button);
    }
    public void OnTabEnter(TabButton button){
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        button.buttonUI.interactable = true;
    }
    public void OnTabExit(TabButton button){
        ResetTabs();
    }
    public void OnTabSelected(TabButton button){
        selectedTab = button;
        ResetTabs();
        button.buttonUI.interactable = false;
        int index = button.transform.GetSiblingIndex();
        for(int i = 0; i < contents.Count; i++){
            if(i==index){
                contents[i].SetActive(true);
            }else{
                contents[i].SetActive(false);
            }
        }
    }
    public void ResetTabs(){
        foreach(TabButton button in tabButtons){
            if(selectedTab != null && button == selectedTab) continue;
            button.buttonUI.interactable = true;
        }
    }
}
