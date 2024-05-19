using UnityEngine;

public class SoundVisualizer : MonoBehaviour
{
    BandSource bandSource;
    MeshRenderer[] groundRenderers;
    Color[] originalColors;
    Vector3[] originalPositions;
    readonly float[] steps = { 0f, 2f, 4f, 6f, 8f, 10f, 12f };

    private void Awake()
    {
        bandSource = FindObjectOfType<BandSource>();
    }

    private void Start()
    {
        GameObject[] grounds = GameObject.FindGameObjectsWithTag("Ground");
        groundRenderers = new MeshRenderer[grounds.Length];
        originalColors = new Color[grounds.Length];
        originalPositions = new Vector3[grounds.Length];
        for (int i = 0; i < grounds.Length; i++)
        {
            groundRenderers[i] = grounds[i].GetComponent<MeshRenderer>();
            originalColors[i] = groundRenderers[i].material.color;
            originalPositions[i] = groundRenderers[i].transform.position;
        }

    }

    private void Update()
    {
        for (int i = 0; i < groundRenderers.Length; i++)
        {
            var renderer = groundRenderers[i];
            Vector3 pos = originalPositions[i];
            float distance = pos.magnitude;
            for (int j = 0; j < steps.Length - 1; j++)
                if (steps[j] < distance && distance < steps[j + 1])
                {
                    float t = Mathf.InverseLerp(steps[j], steps[j + 1], distance);
                    float value = Mathf.Lerp(bandSource.bufferedRelativeBands[j], bandSource.bufferedRelativeBands[j + 1], t);
                    value = Mathf.Pow(value, 1.5f);
                    renderer.material.color = Color.Lerp(originalColors[i], Color.white, value);
                    renderer.transform.position = originalPositions[i] + 0.3f * value * Vector3.up;
                }
            if (steps[^1] < distance)
            {
                float angle = Mathf.Atan2(pos.z, pos.x) * Mathf.Rad2Deg;
                float t = Mathf.InverseLerp(0f, 180f, angle);
                t = Mathf.Abs(t - 1);
                int step = (int)(t * 6);
                float d = t * 6 - step;
                float value = Mathf.Lerp(bandSource.bufferedRelativeBands[step], bandSource.bufferedRelativeBands[step + 1], d);
                var scale = renderer.transform.localScale;
                scale.y = value * 5f + 1f;
                renderer.transform.localScale = scale;
                renderer.transform.position = originalPositions[i];
            }
        }
    }
}
