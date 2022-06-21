using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private bool _isCollected = false;
    private bool _canCollect = false;

    private ItemCollector _itemCollector = null;

    private Transform _itemHolder = null;
    
    private void OnEnable()
    {
        _isCollected = false;
        _canCollect = false;
    }
    private void Start()
    {
        _canCollect = true;
        _itemCollector = FindObjectOfType<ItemCollector>();
    }

    private void Update()
    {
        _canCollect = _itemCollector.currentAmount < _itemCollector.maxAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_canCollect)
        {
            if(_isCollected) return;
        
            if (other.TryGetComponent(out ItemCollector itemCollector))
            {
                itemCollector.AddNewItem(transform);
                _isCollected = true;
            }
        }
    }
}
