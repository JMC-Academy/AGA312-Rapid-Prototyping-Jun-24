using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformMover : MonoBehaviour
{
    float startPos;
    public float tweenTime = 2f;
    public Ease tweenEase = Ease.OutBack;
    float time = 0;
    float changeTime = 5f;
    bool up = false;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.y;
        int rnd = Random.Range(0, 10);
        if (rnd < 5) up = true;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > changeTime)
        {
            time = 0;
            changeTime = Random.Range(4,6);
            MovePlatform();
        }
    }

    void MovePlatform()
    {
        up = !up;
        float moveTo = up ? startPos + 5 : startPos - 5;
        transform.DOMoveY(moveTo, tweenTime).SetEase(tweenEase).OnComplete(()=>TweenX.TweenMainCamera(0.5f, 0.5f));
    }
}
