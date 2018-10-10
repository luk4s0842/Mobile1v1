using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {

    public int movingSpeed;
    public float dashSpeed;
    public float startDashTime;
    private int direction;
    private Rigidbody2D rb;
    private float dashTime;


    private float rightDoubleClickTime;
    private float leftDoubleClickTime;
    public float doubleClickWaitingTime;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }


    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer){
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * movingSpeed;
        transform.Translate(x, 0, 0);


        if(rightDoubleClickTime >= 0){
            rightDoubleClickTime -= Time.deltaTime;
        } else {
            rightDoubleClickTime = 0;
        }
        if (leftDoubleClickTime >= 0) {
            leftDoubleClickTime -= Time.deltaTime;
        } else {
            leftDoubleClickTime = 0;
        }

        if (direction == 0) {
            if (Input.GetKeyDown(KeyCode.A)) {
                if (leftDoubleClickTime > 0) {
                    direction = 1;
                } else {
                    leftDoubleClickTime += doubleClickWaitingTime;
                }
            } else if (Input.GetKeyDown(KeyCode.D)) {
                if (rightDoubleClickTime > 0) {
                    direction = 2;
                } else {
                    rightDoubleClickTime += doubleClickWaitingTime;
                }
            }
        } else {
            if(dashTime <= 0){
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            } else {
                dashTime -= Time.deltaTime;

                if(direction == 1){
                    rb.velocity = Vector2.left * dashSpeed;
                } else if (direction == 2){
                    rb.velocity = Vector2.right * dashSpeed;
                }
            }
        }
	}

    public override void OnStartLocalPlayer() {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
