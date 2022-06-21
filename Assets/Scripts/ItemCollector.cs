using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    [SerializeField]
    private Transform _itemHolder = null;

    [SerializeField] private Transform _itemSeller = null;

    private CropUIManager _cropUIManager = null;
    
    [Header("Amount")]
    public int currentAmount = 0;
    public int maxAmount = 40;
    
    private void Awake()
    {
        _cropUIManager = FindObjectOfType<CropUIManager>();
    }

    public void AddNewItem(Transform itemToAdd)
    {
        if (currentAmount < maxAmount)
        {
            itemToAdd.DOJump(_itemHolder.position + new Vector3(0f, 0.3f * currentAmount, 0f), 1f,1, .1f).OnComplete(
                () => 
                {
                    itemToAdd.SetParent(_itemHolder, true);
                    itemToAdd.localPosition = new Vector3(0, 0.3f * currentAmount, 0);
                    //itemToAdd.position = new Vector3(0, _itemHolder.position.y + 0.3f * currentAmount, 0);
                    itemToAdd.DOPunchPosition(new Vector3(0f, 0.3f, 0f), .3f);
                    itemToAdd.localRotation = Quaternion.identity;
                    
                    currentAmount++;
                    _cropUIManager.UpdateUI(currentAmount, maxAmount);
                } 
            );
        }
    }

    public void RemoveItem(Transform itemToRemove)
    {
        if (currentAmount > 0)
        {
            itemToRemove.DOJump(_itemSeller.position /*+ new Vector3(0f, 0.3f * currentAmount, 0f)*/, 1f,1, 1f).OnComplete(
                () => 
                {
                    itemToRemove.SetParent(null);
                    itemToRemove.localPosition = _itemHolder.transform.position;
                    itemToRemove.localRotation = _itemHolder.transform.rotation;
                    _cropUIManager.UpdateUI(currentAmount, maxAmount);
                    /*
                    currentAmount--;
                */
                } 
            );
        }
    }
}
