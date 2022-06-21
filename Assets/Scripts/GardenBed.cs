using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GardenBed : MonoBehaviour
{
    private bool _isPlanted = false;

    private TextMeshPro _plantedText = null;
    [SerializeField] 
    private GameObject _cropPrefab;

    private void Start()
    {
        _plantedText = GetComponentInChildren<TextMeshPro>();
    }

    private void Update()
    {
        if (_isPlanted)
        {
            _plantedText.gameObject.SetActive(false);
        }
        else _plantedText.gameObject.SetActive(true);
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement) && !_isPlanted)
        {
            Instantiate(_cropPrefab, transform);

            /*_cropPrefab = ObjectPool.SharedInstance.GetPooledObject(); 
            if (_cropPrefab != null) {
                _cropPrefab.transform.position = transform.position;
                _cropPrefab.transform.rotation = transform.rotation;
                _cropPrefab.SetActive(true);
            }*/
            
            /*var cropPlant =  Instantiate(_cropPrefab, transform.position, transform.rotation);
            cropPlant.transform.parent = gameObject.transform;*/
            
            _isPlanted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!GetComponentInChildren<CropStateManager>())
        {
            _isPlanted = false;
        }
    }
}
