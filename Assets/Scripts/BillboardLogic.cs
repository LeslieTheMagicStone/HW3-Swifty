using UnityEngine;

public class BillboardLogic : MonoBehaviour
{
    // Always look at the camera
    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
