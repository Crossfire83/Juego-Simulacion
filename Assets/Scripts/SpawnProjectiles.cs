using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour {

    public GameObject FirePoint;
    public List<GameObject> vfx = new List<GameObject>();
    public GameObject player;

    private GameObject EffectToSpawn;
    private float timeToFire = 0;

	// Use this for initialization
	void Start () {
        EffectToSpawn = vfx[0];
	}
	
	// Update is called once per frame
	void Update () {
        Touch touch = Input.GetTouch(0);
        if ((touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / EffectToSpawn.GetComponent<ProjectileMove>().fireRate;
            SpawnVFX();
        }
        //if (Input.GetMouseButton(0) && Time.time >= timeToFire)
        //{
        //    timeToFire = Time.time + 1 / EffectToSpawn.GetComponent<ProjectileMove>().fireRate;
        //    SpawnVFX();
        //}
	}

    void SpawnVFX()
    {
        GameObject vfx;
        if (FirePoint != null)
        {
            vfx = Instantiate(EffectToSpawn, FirePoint.transform.position, Quaternion.identity);
            if (player != null) {
                vfx.transform.localRotation = player.transform.rotation;
            }
        }
        else {
            Debug.Log("no Fire Point");
        }
    }
}
