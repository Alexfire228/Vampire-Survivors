using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] chunkPrefabs;

    private List<GameObject> chunkList = new List<GameObject>();

    public static MapCreator Instance;

    private float chunkSize;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateChunk(transform.position);

        chunkSize = 24;
    }

    private void GenerateChunk(Vector3 pos)
    {
        int random = Random.Range(0, chunkPrefabs.Length);

        for (int i = 0; i < chunkList.Count; i++)
        {
            if (pos == chunkList[i].transform.position)
            {
                chunkList[i].SetActive(true);
                return;
            }
        }

        chunkList.Add(Instantiate(chunkPrefabs[random], pos, Quaternion.identity));
    }

    public void GenerateNeighborChunks(Vector3 pos)
    {
        chunkSize = 24;
        GenerateChunk(pos + new Vector3(chunkSize, chunkSize, 0));
        GenerateChunk(pos + new Vector3(chunkSize, 0, 0));
        GenerateChunk(pos + new Vector3(chunkSize, -24, 0));
        GenerateChunk(pos + new Vector3(0, chunkSize, 0));
        GenerateChunk(pos + new Vector3(-24, chunkSize, 0));
        GenerateChunk(pos - new Vector3(chunkSize, 0, 0));
        GenerateChunk(pos - new Vector3(chunkSize, chunkSize, 0));
        GenerateChunk(pos - new Vector3(0, chunkSize, 0));

        DeleteUnnecesaryChunks(pos);
    }

    public void DeleteUnnecesaryChunks(Vector3 pos)
    {
        for (int i = 0; i < chunkList.Count; i++)
        {
            if (Vector3.Distance(PlayerCamera.Instance.transform.position, chunkList[i].transform.position) > chunkSize * 1.5f)
                chunkList[i].SetActive(false);

            else
                chunkList[i].SetActive(true);
        }
    }
}
