using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

  private Rigidbody rb;

  private float x_axis;
  private float y_axis;

  private float yaw_speed = 3f;
  private float yaw;

  private float pitch_speed = 3f;
  private float pitch;

  private float curent_speed = 2f;

  private GameObject cam;

  // Start is called before the first frame update
  void Start() {
    rb = GetComponent<Rigidbody>();
    cam = GameObject.Find("Main Camera");
  }

  // Update is called once per frame
  void Update() {
    x_axis = Input.GetAxis("Horizontal");
    y_axis = Input.GetAxis("Vertical");

    yaw += yaw_speed * Input.GetAxis("Mouse X");
    pitch -= pitch_speed * Input.GetAxis("Mouse Y");

    transform.eulerAngles = new Vector3(pitch, yaw, 0f);

    //cam.transform.eulerAngles = new Vector3(pitch, 0f, 0f);
  }

  private void FixedUpdate() {
    rb.MovePosition(transform.position + Time.deltaTime * curent_speed * transform.TransformDirection(x_axis, 0f, y_axis));
  }
}
