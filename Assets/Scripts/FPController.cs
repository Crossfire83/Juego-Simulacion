using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class FPController : MonoBehaviour {

    public float speed = 5.0f;
    public int Points = 0;
    public int Lifes = 5;
    public Text LifesText;
    public Text PointsText;
    public Text GameOverText;
    //private Transform cam;
    //private Rigidbody rb;
    //private Vector3 velocity = Vector3.zero;
    //private float mouseSensitivity = 250f;
    //private float verticalLookRotation;
    private bool locked = true;
    public GameObject Scene;
    private Timer timer;

    // Use this for initialization
    void Start () {
        //rb = GetComponent<Rigidbody>();
        //cam = Camera.main.transform;
        //Cursor.lockState = CursorLockMode.Locked;
        SetLifesText();
        SetPointsText();
        timer = new Timer()
        {
            Interval = 2000
        };
        timer.Elapsed += Timer_Elapsed;
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        timer.Stop();
        GameOverText.text = "";
    }

    // Update is called once per frame
    void Update () {
        
        //float xMove = Input.GetAxisRaw("Horizontal");
        //float yMove = Input.GetAxisRaw("Vertical");
        //float zMove = Input.GetAxisRaw("Jump");

        //Vector3 movHorizontal = transform.right * xMove;
        //Vector3 movVertical = transform.forward * yMove;
        //Vector3 movUp = transform.up * zMove;

        //velocity = (movHorizontal + movVertical + movUp).normalized * speed;


        transform.rotation = GyroToUnity(Input.gyro.attitude);
        //verticalLookRotation += Input.acceleration.y * Time.deltaTime * mouseSensitivity;
        //transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity);
        //verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        //verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        //cam.localEulerAngles = Vector3.left * verticalLookRotation;

        if (Input.GetKeyDown(KeyCode.Escape)) {
            LockCursor();
        }
    }

    private void LockCursor()
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else {
            Cursor.lockState = CursorLockMode.None;
        }
        locked = !locked;
    }

    private void FixedUpdate()
    {
        //if (velocity != Vector3.zero)
        //{
        //    rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        //}
    }

    public void SetLifesText() {
        LifesText.text = "Lifes: " + Lifes.ToString();
        if (Lifes == 0) {
            GameOverText.text = "GAME OVER";
            Scene.GetComponent<SpawnGarbage>().SpawnRate = 0;
        }
    }

    public void SetPointsText()
    {
        PointsText.text = "Points: " + Points.ToString();
        if (Points % 10 == 0 && Points != 0) {
            Scene.GetComponent<SpawnGarbage>().SpawnRate += 0.2f;
            int level = (Points / 10) + 1;
            GameOverText.text = "LEVEL " + level.ToString();
            timer.Start();
        }
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
