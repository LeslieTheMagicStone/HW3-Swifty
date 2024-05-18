using UnityEngine;

public class TrailLogic : MonoBehaviour
{
    [SerializeField] TrailRenderer leftHandTrail;
    [SerializeField] TrailRenderer rightHandTrail;

    public void ShowLeftHandTrail() { leftHandTrail.enabled = true; }
    public void ShowRightHandTrail() { rightHandTrail.enabled = true; }
    public void HideLeftHandTrail() { leftHandTrail.enabled = false; }
    public void HideRightHandTrail() { rightHandTrail.enabled = false; }
}
