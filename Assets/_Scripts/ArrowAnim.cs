using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        LeanTween.moveY(gameObject.GetComponent<RectTransform>(), 0.001f, 0.001f).setEase(LeanTweenType.easeInOutBounce).setDelay(0.1f);

    }

    // Update is called once per frame
    void Update()
    {
        


        //while (true)
        //{
        //    LeanTween.moveY(gameObject.GetComponent<RectTransform>(), 0, 0.1f);
        //    LeanTween.moveY(gameObject.GetComponent<RectTransform>(), 0, -0.1f);
        //}

        //LeanTween.moveY(gameObject, 1f, 1f).setEase(LeanTweenType.easeInQuad).setDelay(0.3f);
    }
}
