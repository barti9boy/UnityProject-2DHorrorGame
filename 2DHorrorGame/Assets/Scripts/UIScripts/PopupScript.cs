using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupScript : MonoBehaviour
{
    public TextMeshProUGUI popupText;
    [SerializeField] private Animator popupAnimator;

    public bool popupIn, popupShow, popupOut, popupHide;
    
    public void Awake()
    {
        popupHide = true;
        popupIn = false;
        popupShow = false;
        popupOut = false;
        UpdateAnimations();
    }

    public void PopupOut()
    {
        popupOut = true;
        popupIn = false;
        popupShow = false;
        popupHide = false;
        UpdateAnimations();
    }
    public void PopupIn()
    {
        popupIn = true;
        popupOut = false;
        popupHide = false;
        popupOut = false;
        UpdateAnimations();
    }
    public void PopupShow()
    {
        popupShow = true;
        popupHide = false;
        popupIn = false;
        popupOut = false;
        UpdateAnimations();
        
    }
    public void PopupHide()
    {
        popupHide = true;
        popupShow = false;
        popupOut = false;
        popupIn = false;
        UpdateAnimations();
        PopupSetInactive();
    }
    public void PopupSetMessage(string message)
    {
        popupText.text = message;
    }
    public void PopupSetActive(string message)
    {
        this.gameObject.SetActive(true);
        PopupSetMessage(message);
        PopupIn();
    }
    public void PopupSetInactive()
    {
        popupText.text = "";
        this.gameObject.SetActive(false);
    }
    public void UpdateAnimations()
    {
        popupAnimator.SetBool("PopupHide", popupHide);
        popupAnimator.SetBool("PopupIn", popupIn);
        popupAnimator.SetBool("PopupShow", popupShow);
        popupAnimator.SetBool("PopupOut", popupOut);
    }
}
