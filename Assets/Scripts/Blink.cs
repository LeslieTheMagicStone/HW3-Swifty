using System.Collections;
using TMPro;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] private float blinkTime;
    IEnumerator Start()
    {
        var tmp = GetComponent<TMP_Text>();

        while (true)
        {
            yield return new WaitForSeconds(blinkTime);
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 0f);
            yield return new WaitForSeconds(blinkTime);
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 1f);
        }
    }
}
