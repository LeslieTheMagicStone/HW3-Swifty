using UnityEngine;

public class PlayerDamageLogic : MonoBehaviour
{
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;

    public void ShowLeftHand() { leftHand.SetActive(true); }
    public void HideLeftHand() { leftHand.SetActive(false); }

    public void ShowRightHand() { rightHand.SetActive(true); }
    public void HideRightHand() { rightHand.SetActive(false); }

}
