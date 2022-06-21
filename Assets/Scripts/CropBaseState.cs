using UnityEngine;
public abstract class CropBaseState
{
   public abstract void EnterState(CropStateManager crop);

   public abstract void UpdateState(CropStateManager crop);

   public abstract void OnTriggerEnter(CropStateManager crop, Collider collision);
}
