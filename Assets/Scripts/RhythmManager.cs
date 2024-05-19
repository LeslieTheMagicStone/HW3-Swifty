using System.IO;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public enum HitType { Perfect, Good, Bad };
public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance => instance;
    private static RhythmManager instance;
    [HideInInspector] public int combo;
    [HideInInspector] public int totalHit;

    public float bpm => _bpm;
    private float _bpm = 60;
    public float Tnote => 60f / _bpm;

    public const float PERFECT_TIME = 0.2f;
    public const float GOOD_TIME = 0.4f;
    public const float BAD_TIME = 0.5f;

    [SerializeField] string mapName;
    [SerializeField] TrackLogic[] tracks;
    [SerializeField] TMP_Text hitStatusText;
    [SerializeField] TMP_Text comboText;
    [SerializeField] XBotLogic[] enemies;
    [SerializeField] TargetLogic[] targets;
    private int targetIndex = 0;
    private HitType lastHitType;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        PrepareMap();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            tracks[0].DetectNote();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            tracks[1].DetectNote();
        // if (Input.GetKeyDown(KeyCode.DownArrow))
        //     tracks[2].DetectNote();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            tracks[3].DetectNote();
    }

    public void Hit(HitType hitType)
    {
        combo++;
        totalHit++;
        lastHitType = hitType;
        hitStatusText.text = hitType.ToString() + "!!";
        comboText.text = "Combo: " + combo.ToString();
    }

    public void Miss()
    {
        combo = 0;
        hitStatusText.text = "";
        comboText.text = "Combo: " + combo.ToString();
    }

    private void PrepareMap()
    {
        string filePath = Path.Combine(Application.dataPath, "Maps", mapName + ".mp");
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            string[] configs = lines[0].Split(' ');
            _bpm = int.Parse(configs[0]);
            string mapstring = string.Concat(lines[1..]);
            float t = 0f;
            bool appending = true;
            for (int i = 0; i < mapstring.Length; i++)
            {
                if (char.IsDigit(mapstring[i]))
                {
                    enemies[mapstring[i] - '0'].Laser(i * Tnote);
                }
                else if (mapstring[i] == 't')
                {
                    int startIndex = mapstring.IndexOf('{', i);
                    int endIndex = mapstring.IndexOf('}', startIndex);
                    string durationStr = mapstring.Substring(startIndex + 1, endIndex - startIndex - 1);
                    targets[targetIndex++].Init(t, float.Parse(durationStr) * Tnote);
                    i = endIndex;
                }
                else switch (mapstring[i])
                {
                    case 'w': tracks[0].SpawnNote(t); break;
                    case 'a': tracks[1].SpawnNote(t); break;
                    // case 's': tracks[2].SpawnNote(t); break;
                    case 'd': tracks[3].SpawnNote(t); break;
                    case '(': appending = false; break;
                    case ')': appending = true; break;
                }

                if (appending) { t += Tnote; }
            }
        }
        else
        {
            Debug.LogError("File not found");
        }
    }
}
