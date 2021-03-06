using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonInMenu : MonoBehaviour
{
    // Cannon firing variables
    Rigidbody cannonballRB;
    public GameObject cannonBall;
    public Transform shotPos;
    public GameObject explosion;
    public float firePower;
    public float fireRate = 2f;
    public int powerMultiplier = 100;
    public float mouseSensitivity = 100.0f;
    public AudioSource fireSound;
    private float rotY;
    private float rotX;
    private float nextTimeToFire = 0f;

    void Start()
    {
        firePower *= powerMultiplier;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    void Update()
    {

        // Get input from mouse
        float mouseX = Input.GetAxis("Mouse X") * 20;
        float mouseY = -Input.GetAxis("Mouse Y") * 20;

        // Apply speed and sensitivity to camera look movement
        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        // Up/Down
        rotX = Mathf.Clamp(rotX, -75, 35);

        // Set starting angle of the cannon
        Quaternion localRotation = Quaternion.Euler(rotX, rotY + 90, 0.0f);
        transform.rotation = localRotation;

        // Reloader logic
        if (Input.GetButtonDown("Fire1"))
        {
            FireCannon();
        }
    }

    public void FireCannon()
    {
        // Instantiate a ball and fire the ball. Play the sound as well.
        GameObject cannonBallCopy = Instantiate(cannonBall, shotPos.position, transform.rotation);
        cannonballRB = cannonBallCopy.GetComponent<Rigidbody>();
        cannonballRB.AddForce(transform.forward * firePower);
        Instantiate(explosion, shotPos.position, shotPos.rotation);
        fireSound.Play();
    }

    public void SetSensitvity(float sense)
    {
        mouseSensitivity = sense;
        PlayerPrefs.SetFloat("SaveSense", mouseSensitivity);
        Debug.Log(sense);
    }
}
