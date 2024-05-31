using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneConfigurator_UI : MonoBehaviour
{
    public BoneToggle_UI Toggle_UI_Prefab;
    public Axis_OSC_Mannequin axisOSCMannequin;
    private List<BoneToggle_UI> allToggles = null;
    
    List<int> ActiveBonePairs = new List<int>();
    // Start is called before the first frame update
    public void OnEnable()
    {
        doOnce = false;
    }
    public void ToggleFromActiveBonePairs()
    {
        ActiveBonePairs = axisOSCMannequin.ActiveBonePairs;
        if (allToggles == null) return;
        for (int i = 0; i < allToggles.Count; i++)
        {
            allToggles[i].SetToggle(false);
        }
        for (int i = 0; i < ActiveBonePairs.Count; i++)
        {
            int indexToToggle = ActiveBonePairs[i];
            allToggles[indexToToggle].SetToggle(true);
            PairTransformToString ptr = axisOSCMannequin.AllPossibleBonesPairs[i];
        }
    }
    public void InitalizeBoneConfigurations()
    {
        if (allToggles != null) return;
        allToggles = new List<BoneToggle_UI>();
        int i = 0;

        foreach(PairTransformToString ptr in axisOSCMannequin.AllPossibleBonesPairs)
        {
            BoneToggle_UI bTUI = GameObject.Instantiate(Toggle_UI_Prefab,this.transform);
            bTUI.SetPairTransformToString(ptr);
            ptr.Init();
            allToggles.Add(bTUI);
            bTUI.SetToggle(true);
            
            i++;
        }
        ToggleFromActiveBonePairs();
        
    }
    public void ConfirmBones()
    {
        ActiveBonePairs.Clear();
        List<PairTransformToString> outPairs = new List<PairTransformToString>();
        for (int i = 0; i < axisOSCMannequin.AllPossibleBonesPairs.Count; i++)
        {
            if(allToggles[i].IsToogled())
            {
                outPairs.Add(axisOSCMannequin.AllPossibleBonesPairs[i]);
                ActiveBonePairs.Add(i);
            }
        }
        axisOSCMannequin.SetPTRsForBones(outPairs);
    }
    bool doOnce = false;
    public void Update()
    {
        if(!doOnce)
        {
            doOnce = true;
            ToggleFromActiveBonePairs();
        }
    }
}
