using System.Collections;
using UnityEngine;

public class Music : MonoBehaviour
{
    public float delay;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        GetComponent<AudioSource>().Play();
    }
}
