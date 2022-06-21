using System.Collections;
using UnityEngine;

public class CropHarvestingState : CropBaseState
{
    private readonly Vector3 _startCropScale = new Vector3(1f, 250f, 1f);
    private readonly Vector3 _offset = new Vector3(0f, 1.5f, 0f);

    private int _timesToHarvest = 3;
    public override void EnterState(CropStateManager crop)
    {
        crop.transform.localScale = _startCropScale;
        crop.cropModel.SetActive(false);
        Object.Instantiate(crop.sliceableModel, crop.transform);
    }
    
    public override void UpdateState(CropStateManager crop)
    { }

    public override void OnTriggerEnter(CropStateManager crop, Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Scythe scythe))
        {
            if (crop.objectSlicerSamples.Length >= _timesToHarvest)
            {
                ObjectPool.Instance.Activate(0, crop.transform.position + _offset, crop.transform.rotation);
                
                Object.Destroy(crop.gameObject);
                crop.SwitchState(crop._plantState);
            
                /*crop.collectable = ObjectPool.SharedInstance.GetPooledObject(); 
                if (crop.collectable != null) {
                    crop.collectable.transform.position = crop.transform.position;
                    crop.collectable.transform.rotation = crop.transform.rotation;
                    crop.collectable.SetActive(true);
                }
                crop.cropGameObject.SetActive(false);*/

                /*obj.Instantiate(crop.collectable, crop.transform.position, crop.transform.rotation);
                obj.Destroy(crop.cropGameObject);*/
            
                //crop.cropGameObject.transform.GetComponentInParent<GardenBed>().isPlanted = false;
            }
        }
    }
}
