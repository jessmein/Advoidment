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
    private int rimHitCounter = 0;
    private int BucketsCounter = 0;
    private int missCounter = 0;
    private Vector3 resetPos;
    [SerializeField] TMP_Text bucketText;
    [SerializeField] TMP_Text rimText;
    [SerializeField] TMP_Text missText;
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

        resetPos = new Vector3(
                //sling.transform.position.x - (basketball.transform.localScale.x / 2.0f),
                sling.transform.position.x - (basketball.transform.localScale.x / 2.0f) /** (basketball.transform.localScale.x / 5.0f)*/,
                sling.transform.position.y /** (basketball.transform.localScale.y / 5)*/,
                sling.transform.position.z
                );
    }
    // Update is called once per frame
    void Update() {
        if (mouseDown) {
            DragBall();
        }

        resetPos = new Vector3(
        //sling.transform.position.x - (basketball.transform.localScale.x / 2.0f),
        sling.transform.position.x - (basketball.transform.localScale.x / 2.0f)/** (basketball.transform.localScale.x / 5.0f)*/,
        sling.transform.position.y /** (basketball.transform.localScale.y / 5)*/,
        sling.transform.position.z
        );
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer.ToString() == "6")//Miss!
        {
            Debug.Log(collision.gameObject.layer.ToString());
            //Vector3 resetPos = new Vector3(162, 145, 0);

            transform.position = resetPos;
            ballRB.isKinematic = false;
            ballSpringJoint.enabled = true;
            ballRB.velocity = new Vector2(0, 0);
            ballRB.angularVelocity = 0.0f;
            missCounter++;
            missText.text = "Miss Counter: " + missCounter;
        } else if (collision.gameObject.layer.ToString() == "7")//rimHit!
          {
            Debug.Log(collision.gameObject.layer.ToString());
            rimHitCounter++;
            rimText.text = "RimHit Counter:" + rimHitCounter;
        } else if (collision.gameObject.layer.ToString() == "8")//hit
          {
            Debug.Log(collision.gameObject.layer.ToString());
            transform.position = resetPos;
            ballRB.isKinematic = false;
            ballSpringJoint.enabled = true;
            BucketsCounter++;
            bucketText.text = "Buckets! Counter: " + BucketsCounter;
            GameObject parent = GameObject.Find("BasketBallAd(Clone)");
            BasketBallAd parentBasket = parent.GetComponent<BasketBallAd>();
            hit = true;
            Debug.Log(parent);
        }
    }

    void DragBall() {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);


        float distacne = Vector2.Distance(mousePos, slingRb.transform.position);

        if (distacne >= maxDragDistance) {
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
