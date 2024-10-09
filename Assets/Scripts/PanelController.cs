using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public TMP_Text nameText;
    public Slider volSlider;
    public TMP_Text valueText;

    public UnityAction<PanelController> onSlierValueChangedAction;

    private string userName = "";
    private bool resetting = false;

    public void OnSliderValueChanged()
    {
        if (!resetting)
        {
            valueText.SetText(volSlider.value + "%");
            onSlierValueChangedAction(this);
        }
    }

    public void ResetState()
    {
        resetting = true;
        nameText.SetText("");
        volSlider.value = 100;
        valueText.SetText("100%");
        resetting = false;
    }

    public void SetSliderValue(float value)
    {
        resetting = true;
        volSlider.value = value;
        valueText.SetText(volSlider.value + "%");
        resetting = false;
    }

    public float GetSliderValue()
    { 
        return volSlider.value;
    }

    public void Activate(bool active)
    {
        this.gameObject.SetActive(active);
    }

    public void SetUserName(string name)
    {
        userName = name;
    }

    public void SetDisplayName(string name)
    {
        this.nameText.SetText(name);
    }

    public string GetUserName()
    {
        return userName;
    }
}
