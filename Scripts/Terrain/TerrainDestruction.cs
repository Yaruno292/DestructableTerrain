using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDestruction : MonoBehaviour {

    [SerializeField]
    private Terrain terrain;

    private TerrainData terrainData;

    private Vector3 grenadePos;

    private float[,] heights;

	// Use this for initialization
	void Start () {

        if (terrain == null)
        {
            terrain = FindObjectOfType<Terrain>();
        }

        terrainData = terrain.terrainData;
    }

    public void DestroyTerrain()
    {
        grenadePos = this.transform.position;

        heights = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);

        int terX = (int)((grenadePos.x / terrainData.size.x) * terrainData.heightmapWidth);
        int terZ = (int)((grenadePos.z / terrainData.size.z) * terrainData.heightmapHeight);
        float y = heights[terX, terZ];
        y = 0.0015f * grenadePos.y;
        float[,] height = new float[2, 2];
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                height[i, j] = y;
                Debug.Log(height);
            }
        }
        heights[terX, terZ] = y;
        terrainData.SetHeights(terX, terZ, height);
        Debug.Log(y);
        Destroy(gameObject);
    }
}
