using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour {

    public float speed;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;

    // Use this for initialization
    void Start () {
        if (muzzlePrefab != null) {
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;
            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null) {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
        var psBullet = gameObject.GetComponent<ParticleSystem>();
        if (psBullet != null)
        {
            Destroy(gameObject, psBullet.main.duration);
        }
        else
        {
            var psChild = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(gameObject, psChild.main.duration);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else {
            Debug.Log("No speed");
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        speed = 0;

        if (hitPrefab != null)
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;
            var hitVFX = Instantiate(hitPrefab, position, rotation);
            var psHit = hitVFX.GetComponent<ParticleSystem>();
            if (psHit != null)
            {
                Destroy(hitVFX, psHit.main.duration);
            }
            else
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
        }
        if (collision.gameObject.tag == "Can") {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
