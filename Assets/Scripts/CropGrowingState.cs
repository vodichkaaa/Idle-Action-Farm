using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class CropGrowingState : CropBaseState
{
    private readonly Vector3 _desiredScale = new Vector3(1f, 250f, 1f);
    
    public override void EnterState(CropStateManager crop)
    {
        crop.transform.DOScale(_desiredScale, 10f).SetEase(Ease.InOutSine);
    }
    
    public override void UpdateState(CropStateManager crop)
    {
        if (crop.transform.localScale.y >= 250)
        {
            crop.SwitchState(crop._harvestingState);
        }
        
    }

    public override void OnTriggerEnter(CropStateManager crop, Collider collision)
    { }
}
