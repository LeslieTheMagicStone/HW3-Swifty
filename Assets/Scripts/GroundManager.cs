using UnityEngine;

public class GroundManager : MonoBehaviour
{
    [SerializeField] float xVoxelCount;
    [SerializeField] float yVoxelCount;
    [SerializeField] float zVoxelCount;
    [SerializeField] Transform groundParent;
    [SerializeField] GameObject voxelPrefab;
    const float GROUND_UPPER_POS_Y = 0f;

    private void Awake()
    {
        InitGround();
    }

    private void InitGround()
    {
        Vector3 orig = new(-xVoxelCount / 2, GROUND_UPPER_POS_Y - voxelPrefab.transform.localScale.y / 2, -zVoxelCount / 2);
        for (int i = 0; i < xVoxelCount; i++)
            for (int j = 0; j < yVoxelCount; j++)
                for (int k = 0; k < zVoxelCount; k++)
                {
                    Vector3 pos = new Vector3(i, -j, k) + orig;
                    var voxel = Instantiate(voxelPrefab, pos, Quaternion.identity);
                    voxel.transform.SetParent(groundParent);
                }
    }

}
