using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public SkinShopUI skinShopUI;
    public Button buttonUI;
    private void Start() {
        skinShopUI.Subscribe(this);
    }
    public void OnPointerEnter(PointerEventData eventData){
        skinShopUI.OnTabEnter(this);
    }
    public void OnPointerClick(PointerEventData eventData){
        skinShopUI.OnTabSelected(this);
    }
    public void OnPointerExit(PointerEventData eventData){
        skinShopUI.OnTabExit(this);
    }
}
