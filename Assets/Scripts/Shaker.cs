using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : Singleton<Shaker> {

    float shake  = 0;
    float shakeAmount  = 0.5f;
    float decreaseFactor  = 1.0f;
    float shakeDecreaseFactor = 0.05f;
    Coroutine shaker;

    protected Shaker() { }

    void Update()
    {
       
            if (Input.GetKeyDown(KeyCode.Space))
                ShakeIt(0.3f);
        
    }

    private IEnumerator _ShakeIt()
    {
        Camera iCamera = Camera.main;
        Vector3 startPos = iCamera.transform.localPosition;
        Vector2 v = Random.insideUnitCircle * shakeAmount;
        Vector3 oldPoint = iCamera.transform.localPosition;
        Vector3 newPoint = new Vector3(v.x, v.y, 0) + iCamera.transform.localPosition;
        while (shake > 0)
        {
            oldPoint = iCamera.transform.localPosition;
            v = Random.insideUnitCircle * shakeAmount;
            newPoint = new Vector3(v.x, v.y, 0) + iCamera.transform.localPosition;
            float t = 0.33f;
            while(t <= 1.0f) {
                iCamera.transform.localPosition = Vector3.Lerp(oldPoint, newPoint,t);
                Instance.shake -= Time.deltaTime * decreaseFactor;
                Instance.shakeAmount -= Time.deltaTime * shakeDecreaseFactor;
                t += 0.33f;
                yield return new WaitForEndOfFrame();
            }
        }
        Camera.main.transform.localPosition = startPos;
        shaker = null;
    }

    public static void ShakeIt(float amount){
        if (amount != 0)
        {
            Instance.shake = amount;
            if (Instance.shakeAmount > 0.1f)
                Instance.shakeAmount += 1;
            else
                Instance.shakeAmount = 0.5f;
            Instance.shakeDecreaseFactor = Instance.shakeAmount / Instance.shake;
            if(Instance.shaker==null) Instance.shaker = Instance.StartCoroutine(Instance._ShakeIt());
        }
    }
	
	
}
