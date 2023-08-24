using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusFolder : MonoBehaviour
{
    [SerializeField] GameObject[] VirusPrefabs;
    public void SpwanVirus()
    {
        int v = Random.Range(0, VirusPrefabs.Length);
        GameObject virus = Instantiate(VirusPrefabs[v], transform);
        virus.transform.position = new Vector3(Random.Range(5.6f, 48.45f), 15f, 0f);
    }
}
