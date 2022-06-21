using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropPlantState : CropBaseState
{
    private readonly Vector3 _startCropScale = new Vector3(1f, 1f, 1f);

    public override void EnterState(CropStateManager crop)
    {
        crop.transform.localScale = _startCropScale;
    }
    
    public override void UpdateState(CropStateManager crop)
    { }

    public override void OnTriggerEnter(CropStateManager crop, Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            crop.SwitchState(crop._growingState);
        }
    }
}
