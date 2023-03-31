using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private float speed;
    private Vector3 localScale;
    private float spriteWidth;

    public GroundType gType;

    // determines the speed of parallax
    public enum GroundType
    {
        foreground,
        midground,
        background
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (gType)
        {
            case GroundType.foreground:
                speed = -180.0f;
                break;

            case GroundType.midground:
                speed = -100.0f;
                break;

            case GroundType.background:
                speed = -20.0f;
                break;
        }

        Sprite objSprite = GetComponent<SpriteRenderer>().sprite;
        spriteWidth = objSprite.texture.width / objSprite.pixelsPerUnit;

        localScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        localScale = this.transform.localScale;
        Scroll();
        ResetParallax();
    }

    private void Scroll()
    {
        float move = speed * Time.deltaTime;
        this.transform.position += new Vector3(move, 0f, 0f);
    }

    private void ResetParallax()
    {
        Debug.Log(this.name);
        Vector3 pos = Vector3.Scale(this.transform.position, localScale);
        Debug.Log("(Mathf.Abs(pos.x) - spriteWidth) < 0: " + ((Mathf.Abs(pos.x) - spriteWidth) < 0));
        Debug.Log("Mathf.Abs(pos.x) = " + Mathf.Abs(pos.x));
        Debug.Log("Mathf.Abs(pos.x) - spriteWidth = " + (Mathf.Abs(pos.x) - spriteWidth));
        if ((Mathf.Abs(pos.x) - spriteWidth) < 0)
        {
            this.transform.position = new Vector3(0.0f, pos.y, pos.z);
        }

        Debug.Log(pos);
    }
}
