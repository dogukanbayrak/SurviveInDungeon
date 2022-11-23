using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRawImage : MonoBehaviour
{

    public float verticalSpeed;

    RawImage rawImage;



    void Start()
    {
        rawImage=GetComponent<RawImage>();
    }

    void Update()
    {
        Rect currentUv = rawImage.uvRect;

        currentUv.y -= Time.deltaTime * verticalSpeed;

        if(currentUv.y<= -2f)
        {
            currentUv.y = 0f;
        }

        rawImage.uvRect = currentUv;
    }
}
