using System.IO;
using UnityEngine;
using DG.Tweening;
using TMPro;

public enum HitType { Perfect, Good, Bad };
public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance => instance;
    private static RhythmManager instance;
    [HideInInspector] public int combo;
    [HideInInspector] public int totalHit;

    public float bpm => _bpm;
    private float _bpm = 60;

    public const float PERFECT_TIME = 0.2f;
    public const float GOOD_TIME = 0.4f;
    public const float BAD_TIME = 0.5f;

    [SerializeField] string mapName;
    [SerializeField] TrackLogic[] tracks;
    [SerializeField] TMP_Text hitStatusText;
    [SerializeField] TMP_Text comboText;
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
        if (Input.GetKeyDown(KeyCode.DownArrow))
            tracks[2].DetectNote();
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
            string[] line = File.ReadAllLines(filePath);
            string mapstring = line[0];
            for (int i = 0; i < mapstring.Length; i++)
            {
                if (mapstring[i] == ' ') continue;
                switch (mapstring[i])
                {
                    case 'w': tracks[0].SpawnNote(i * 0.5f); break;
                    case 'a': tracks[1].SpawnNote(i * 0.5f); break;
                    case 's': tracks[2].SpawnNote(i * 0.5f); break;
                    case 'd': tracks[3].SpawnNote(i * 0.5f); break;
                }
            }
        }
        else
        {
            Debug.LogError("File not found");
        }
    }
}
