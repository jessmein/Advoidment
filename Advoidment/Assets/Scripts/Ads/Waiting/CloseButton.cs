using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CloseButton : MonoBehaviour
{
    public bool isClicked;
    public Vector3 originalPosition;
    public WaitingAd waitingAd;
    private Vector3 scale;
    SpriteRenderer sprender;

    // Start is called before the first frame update
    void Start()
    {
        scale = waitingAd.transform.localScale;
        float[] pointsX = { -2.8f * scale.x, 2.8f * scale.x };
        float[] pointsY = { -4.7f * scale.y, 4.7f * scale.y };

        isClicked = false;
        transform.localPosition = Vector3.zero;
        originalPosition = transform.position;

        transform.position = new Vector3(originalPosition.x + pointsX[Random.Range(0,2)], originalPosition.y + pointsY[Random.Range(0,2)], 0);
        sprender = gameObject.GetComponent<SpriteRenderer>();
        sprender.enabled = false;
        StartCoroutine(waiter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (sprender.enabled == true)
        {
            Debug.Log("Click");
            isClicked = true;
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.1f);
        sprender.enabled = true;
    }
}
