using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] chunkPrefabs;

    public static MapCreator Instance;

    void Start()
    {
        Instance = this;
        GenerateChunk(new Vector3(0, 0, 0));
    }
    private void GenerateChunk(Vector3 pos)
    {
        int random = Random.Range(0, chunkPrefabs.Length);

        Instantiate(chunkPrefabs[random], pos, Quaternion.identity);
    }

    public void GenerateNeighborChunks(Vector3 pos)
    {
        int random = Random.Range(0, chunkPrefabs.Length);

        float chunkSize = 24;
        GenerateChunk(pos + new Vector3(chunkSize, chunkSize, 0));
        GenerateChunk(pos + new Vector3(chunkSize, 0, 0));
        GenerateChunk(pos + new Vector3(chunkSize, -24, 0));
        GenerateChunk(pos + new Vector3(0, chunkSize, 0));
        GenerateChunk(pos + new Vector3(-24, chunkSize, 0));
        GenerateChunk(pos - new Vector3(chunkSize, 0, 0));
        GenerateChunk(pos - new Vector3(chunkSize, chunkSize, 0));
    }
}
