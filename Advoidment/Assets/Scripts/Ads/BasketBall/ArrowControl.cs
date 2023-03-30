using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject arrowObj;
    private GameObject basketball;
    private GameObject sling;
    private ShootBall shootBallInstance;
    void Start()
    {
        //arrowObj = GameObject.Find("Arrow");
        basketball = GameObject.Find("Basketball");
        sling = GameObject.Find("Sling");
        shootBallInstance = basketball.GetComponent<ShootBall>();
    }

    // Update is called once per frame
    void Update()
    {

        if (shootBallInstance.mouseDown)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            CalculateArrow();
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }
    }

    void CalculateArrow()
    {
        //Calculating midpoint
        Vector3 midpoint = new Vector3((basketball.transform.localPosition.x + sling.transform.localPosition.x) / 2f, (basketball.transform.localPosition.y + sling.transform.localPosition.y) / 2f, 0);

        float distance = Vector2.Distance(basketball.transform.localPosition, sling.transform.localPosition);
        transform.localPosition = midpoint;

        float angleRad = Mathf.Atan2(basketball.transform.localPosition.y - sling.transform.localPosition.y, basketball.transform.localPosition.x - sling.transform.localPosition.x);
        Debug.Log(angleRad);

        float AngleDeg = ((180 / Mathf.PI) * angleRad) + 180;
        transform.localScale = new Vector3(distance, 40, 0);



        /*Vector2 direction = basketball.transform.position - sling.transform.position;
        direction = direction.normalized;*/

        //Vector3.Angle(basketball.transform.position,sling.transform.position);
        transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

    }
}
