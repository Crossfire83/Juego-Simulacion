using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashController : MonoBehaviour {

    private int IsActive = 1;
    public GameObject player;
    public float duration = 5.0f;
    private bool collisionedBefore = false;

    // Update is called once per frame
    void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * IsActive * Time.deltaTime);
	}

    private void OnCollisionEnter(Collision collision)
    {
        IsActive = 0;
        if (collision.gameObject.tag == "Bullet")
        {
            if (!collisionedBefore)
            {
                player.GetComponent<FPController>().Points++;
                player.GetComponent<FPController>().SetPointsText();
            }
            Destroy(gameObject);
        }
        else
        {
            if (collision.gameObject.tag == "Active" && !collisionedBefore)
            {
                player.GetComponent<FPController>().Lifes--;
                player.GetComponent<FPController>().SetLifesText();
                collisionedBefore = true;
            }
            Destroy(gameObject, duration);
        }
        
    }
}
