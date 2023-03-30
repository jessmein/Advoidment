using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(Rigidbody2D))] //never knew you could do this :D
public class ShootBall : BasketBallAd {
    public bool mouseDown;
    private Rigidbody2D ballRB;
    private Camera mainCamera;
    private SpringJoint2D ballSpringJoint;
    private float releaseFequency;
    private float maxDragDistance = 90;
    private Rigidbody2D slingRb;
    private GameObject sling;
    private GameObject basketball;
    private Vector3 resetPos;
    public bool hit = false;

    private void Start() {
        mouseDown = false;
        mainCamera = Camera.main;
        ballRB = this.GetComponent<Rigidbody2D>();
        ballSpringJoint = this.GetComponent<SpringJoint2D>();
        releaseFequency = 1 / (ballSpringJoint.frequency * 4);
        sling = GameObject.Find("Sling");
        basketball = GameObject.Find("Basketball");
        slingRb = sling.GetComponent<Rigidbody2D>();

        //Default basketball position
        resetPos = new Vector3(272.62f, 138.21f, 0.0f);
    }
    // Update is called once per frame
    void Update() {
        if (mouseDown) {
            DragBall();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer.ToString() == "6")//Miss!
        {
            //Debug.Log(collision.gameObject.layer.ToString());
            //Vector3 resetPos = new Vector3(162, 145, 0);

            transform.position = resetPos;
            ballRB.isKinematic = false;
            ballSpringJoint.enabled = true;
            ballRB.velocity = new Vector2(0, 0);
            ballRB.angularVelocity = 0.0f;
        //} else if (collision.gameObject.layer.ToString() == "7")//rimHit!
        //  {
        //    Debug.Log(collision.gameObject.layer.ToString());
        } else if (collision.gameObject.layer.ToString() == "8")//hit
          {
            //Debug.Log(collision.gameObject.layer.ToString());
            //transform.position = resetPos;
            ballRB.isKinematic = false;
            ballSpringJoint.enabled = true;
            GameObject parent = GameObject.Find("BasketBallAd(Clone)");
            BasketBallAd parentBasket = parent.GetComponent<BasketBallAd>();
            hit = true;
            Debug.Log(parent);
        }
    }

    void DragBall() {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);


        float distance = Vector2.Distance(mousePos, slingRb.transform.position);

        if (distance >= maxDragDistance) {
            Vector2 direction = mousePos - slingRb.position;
            direction = direction.normalized;
            ballRB.position = slingRb.position + direction * maxDragDistance;
        } else {
            ballRB.position = mousePos;
        }


    }

    private void OnMouseDown() {
        mouseDown = true;
        ballRB.isKinematic = true;
    }

    private void OnMouseUp() {
        mouseDown = false;
        ballRB.isKinematic = false;
        StartCoroutine(ReleaseBall());
    }

    private IEnumerator ReleaseBall() {
        yield return new WaitForSeconds(releaseFequency);
        ballSpringJoint.enabled = false;
    }
}
