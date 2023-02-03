using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareAd : MonoBehaviour
{
    public GameObject target;
    public GameObject key;

    Collider2D targetCollider, keyCollider;
    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            targetCollider = target.GetComponent<Collider2D>();   
        }

        if (key != null)
        {
            keyCollider= key.GetComponent<Collider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (keyCollider.bounds.Intersects(targetCollider.bounds))
        {
            if (key.GetComponent<Drag>().dragging == false)
            {
                key.transform.position = target.transform.position;
                StartCoroutine(waiter());
                
            }
            
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("BYE");
        Destroy(gameObject);
    }
}
