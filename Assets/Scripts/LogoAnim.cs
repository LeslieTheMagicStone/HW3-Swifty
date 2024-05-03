using System.Collections;
using TMPro;
using UnityEngine;

public class LogoAnim : MonoBehaviour
{
    TMP_Text logoName;
    const float DURATION = 1.0f;

    IEnumerator Start()
    {
        logoName = GetComponent<TMP_Text>();
        float timer = 0.0f;

        while (timer < DURATION)
        {
            timer += Time.deltaTime;
            float scaler = timer / DURATION;
            Color color = logoName.color;
            color.a = scaler;
            logoName.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(1);

        SceneController.Instance.LoadNextScene();
    }

}
