using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class CoinUIManager : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _coinsText = null;
    [SerializeField] 
    private GameObject _animatedCoin = null;
    [SerializeField] 
    private Transform _coinTarget = null;
    [SerializeField] 
    private GameObject _coinArea = null;

    [Space] [Header("Available Coins")] 
    [SerializeField]
    private int _maxCoins = 0;
    private Queue<GameObject> _coinsQueue = new Queue<GameObject>();

    [Space] [Header("Animation Settings")] 
    [SerializeField] [Range(0.5f, 0.9f)]
    private float minAnimDuration = 0f;
    [SerializeField] [Range(0.9f, 2f)]
    private float maxAnimDuration = 0f;
    
    private Vector3 _targetPos = Vector3.zero;

    private int _coins = 0;

    private int Coins
    {
        get => _coins;
        set
        {
            _coins = value;
            _coinsText.text = Coins.ToString();
        }
    }

    private void Awake()
    {
        _targetPos = _coinTarget.position;
        
        SpawnCoin();
    }

    private void SpawnCoin()
    {
        for (var i = 0; i < _maxCoins; i++)
        {
            var coin = Instantiate(_animatedCoin, transform, true);
            coin.SetActive(false);
            _coinsQueue.Enqueue(coin);
        }
    }

    private void CoinAnimate(Vector3 collectedCoinPos, int amount)
    {
        if (_coinsQueue.Count > 0)
        {
            var coin = _coinsQueue.Dequeue();
            coin.SetActive(true);

            coin.transform.position = collectedCoinPos;

            var duration = UnityEngine.Random.Range(minAnimDuration, maxAnimDuration);
            coin.transform.DOMove(_targetPos, duration)
                .SetEase(Ease.InOutBack)
                .OnComplete(() =>
                {
                    coin.SetActive(false);
                    _coinsQueue.Enqueue(coin);

                    Coins += amount;
                    _coinArea.transform.DOPunchPosition(new Vector3(0f, 2f, 0f), 1f);
                });
            
        }
    }

    public void AddCoins(Vector3 collectedCoinPos, int amount)
    {
        CoinAnimate(collectedCoinPos, amount);
    }
}
