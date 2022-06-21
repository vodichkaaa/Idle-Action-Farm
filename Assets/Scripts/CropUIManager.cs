using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class CropUIManager : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _cropText = null;

    public void UpdateUI(int amount, int maxAmount)
    {
        _cropText.text = $"{amount}/{maxAmount}";
        //_cropArea.transform.DOPunchPosition(new Vector3(0f, 1f, 0f), 0.5f);
    }
}
