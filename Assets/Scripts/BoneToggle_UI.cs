using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BoneToggle_UI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI LabelName;
    public Action Toogled;
    private UnityEngine.UI.Toggle toogleObj;
    public void Start()
    {
        toogleObj = this.GetComponent<UnityEngine.UI.Toggle>();
    }
    public bool IsToogled()
    {
        if (toogleObj == null) return false;
        return toogleObj.isOn;
    }
    public void SetToggle(bool isToggleOn)
    {
        if (toogleObj == null) return;
        toogleObj.isOn = isToggleOn;
    }
    public void SetPairTransformToString(PairTransformToString pTR)
    {
        LabelName.text = pTR.vmc_bone_name;
      
    }
    public void ToggledEvent()
    {
        Toogled?.Invoke();
    }
}
