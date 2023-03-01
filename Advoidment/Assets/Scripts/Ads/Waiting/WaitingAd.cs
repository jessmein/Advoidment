using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaitingAd : MonoBehaviour
{
    public CloseButton closeButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (closeButton.isClicked)
        {
            Destroy(gameObject);
            
        }
    }
}
