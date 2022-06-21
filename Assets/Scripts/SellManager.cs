using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellManager : MonoBehaviour
{
    [SerializeField]
    private int _sellAmount = 15;
    [SerializeField]
    private GameObject _coinsPosition = null;
    
    private CoinUIManager _coinUIManager = null;
    
    private Vector3 _coinsScreenPosition = Vector3.zero;

    private void Awake()
    {
        _coinUIManager = FindObjectOfType<CoinUIManager>();
    }

    private void Update()
    {
        _coinsScreenPosition = Camera.main.WorldToScreenPoint(_coinsPosition.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ItemController itemController))
        {
            _coinUIManager.AddCoins(_coinsScreenPosition, _sellAmount);
            
            ObjectPool.Deactivate(other.gameObject);
            
            //other.gameObject.SetActive(false);
        }
    }
}
