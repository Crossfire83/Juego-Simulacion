using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGarbage : MonoBehaviour {

    public List<GameObject> trash = new List<GameObject>(5);
    public float SpawnRate;
    private float SpawnSpeed;
    public GameObject player;
    private System.Random r = new System.Random((int)DateTime.Now.Ticks);

    void Start() {
        SpawnSpeed = Time.time + 1 / SpawnRate;
    }

    // Update is called once per frame
    void Update () {
        if (Time.time >= SpawnSpeed)
        {
            SpawnSpeed = Time.time + 1 / SpawnRate;
            SpawnTrash();
        }
    }

    private void SpawnTrash()
    {
        GameObject instance;
        GameObject trashPrefab = trash[r.Next(5)];
        Vector3 location;
        float z = (float)(r.NextDouble() * 25);
        float x = (float)(r.NextDouble() * 25) - 9.25f;
        location = new Vector3(x,50f,z);
        instance = Instantiate(trashPrefab, location, Quaternion.identity);
        instance.GetComponent<TrashController>().player = player;
    }
}
