using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SellPlate : MonoBehaviour
{
    private Vector3 _startScale = Vector3.zero;
    private Vector3 _desiredScale = new Vector3(1.3f, 0.1f, 1.3f);

    public ItemController[] _itemsToSell = null;

    private void Start()
    {
        _startScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ItemCollector itemCollector))
        {
            gameObject.transform.DOScale(_desiredScale, 0.2f).SetEase(Ease.InElastic);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out ItemCollector itemCollector))
        {
            _itemsToSell = itemCollector.transform.GetComponentsInChildren<ItemController>();

            //itemCollector.RemoveItem(itemCollector.gameObject.GetComponentInChildren<ItemController>(true).transform);

            for (int k = 0; k < _itemsToSell.Length; k++)
            {
                itemCollector.RemoveItem(_itemsToSell[_itemsToSell.Length - 1].transform);
            }
            itemCollector.currentAmount = _itemsToSell.Length;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ItemCollector itemCollector))
        {
            gameObject.transform.DOScale(_startScale, 0.2f).SetEase(Ease.OutElastic);
        }
    }
}
