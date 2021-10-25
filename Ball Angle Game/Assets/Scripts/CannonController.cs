using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CannonController : MonoBehaviour
{
    // Cannon Firing Variables
    public AudioSource fireSound;
    public GameObject cannonBall;
    public GameObject disableCursor;
    Rigidbody cannonballRB;
    public Transform shotPos;
    public GameObject explosion;
    public float firePower;
    public int powerMultiplier = 100;
    public float mouseSensitivity = 100.0f;
    private float rotY;
    private float rotX;

    public float fireRate = 2f;
    private float nextTimeToFire = 0f;

    public ReloadLoader reloadLoader;
    void Start()
    {

        firePower *= powerMultiplier;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause_Menu.GameIsPaused == true)
        {
            disableCursor.SetActive(false);
            Cursor.visible = (true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            disableCursor.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = (false);
        }

        //Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        float mouseX = Input.GetAxis("Mouse X") * 20;
        float mouseY = -Input.GetAxis("Mouse Y") * 20;

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        // Up/Down
        rotX = Mathf.Clamp(rotX, -75, 75);

        // Left/Right
        rotY = Mathf.Clamp(rotY, -115, 115);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY + 90, 0.0f);
        transform.rotation = localRotation;


        if (Pause_Menu.GameIsPaused == false && Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {

            Debug.Log(Time.time + " " + nextTimeToFire);
            nextTimeToFire = Time.time + fireRate;
            reloadLoader.LoadReload(nextTimeToFire, fireRate);
            FireCannon();
        }
    }

    public void FireCannon()
    {
        GameObject cannonBallCopy = Instantiate(cannonBall, shotPos.position, transform.rotation);
        cannonballRB = cannonBallCopy.GetComponent<Rigidbody>();
        cannonballRB.AddForce(transform.forward * firePower);
        Instantiate(explosion, shotPos.position, shotPos.rotation);

        fireSound.Play();

    }
}
