using TMPro;
using UnityEngine;

public class LocData : MonoBehaviour
{
    [SerializeField] private string locKey;

    private void Start()
    {
        if (TryGetComponent(out TextMeshProUGUI tmp))
            tmp.text = LocalizationManager.Instance.GetLocString(locKey);
    }
}
