using System;
using System.Collections;
using System.Collections.Generic;
using BzKovSoft.ObjectSlicer.Samples;
using UnityEngine;
using UnityEngine.UIElements;

public class CropStateManager : MonoBehaviour
{
    public CropBaseState _currentState;
    
    public CropPlantState _plantState = new CropPlantState();
    public CropGrowingState _growingState = new CropGrowingState();
    public CropHarvestingState _harvestingState = new CropHarvestingState();
    
    [Header("Objects")]
    public GameObject cropGameObject = null;
    public GameObject cropModel = null;
    public GameObject sliceableModel = null;
    
    private MeshRenderer _renderer = null;
    
    [Header("Materials")]
    [SerializeField] 
    private Material _cropMaterial = null;
    [SerializeField] 
    private Material _wheatMaterial = null;
    
    [Header("Sliceable Objects")]
    public ObjectSlicerSample[] objectSlicerSamples = null;

    private void Start()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();
        
        cropGameObject = transform.gameObject;
        _currentState = _plantState;
        _renderer.material = _cropMaterial;
        
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
        
        objectSlicerSamples = GetComponentsInChildren<ObjectSlicerSample>(true);

        for (int i = 0; i < objectSlicerSamples.Length; i++)
        {
            if (i != objectSlicerSamples.Length - 1)
            {
                objectSlicerSamples[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _currentState.OnTriggerEnter(this, other);
    }

    public void SwitchState(CropBaseState state)
    {
        if (state == _harvestingState)
        {
            _renderer.material = _wheatMaterial;
        }
        else _renderer.material = _cropMaterial;
        
        _currentState = state;
        state.EnterState(this);
    }
}
