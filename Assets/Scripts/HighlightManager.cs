using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightManager : MonoBehaviour {

    private GameObject last = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Camera mainCamera = Camera.main;
        RaycastHit hit = new RaycastHit();
        if (
            !Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
                             mainCamera.ScreenPointToRay(Input.mousePosition).direction, out hit, 2f,
                             Physics.DefaultRaycastLayers))
        {
            if (last != null)
            {
                last.GetComponent<HoverHighlighter>().HighlightOff();
                last = null;
            }
            return;
        }
        if (last == hit.collider.gameObject) { return; }
        if (hit.collider.GetComponent<HoverHighlighter>())
        {
            if (last != null)
            {
                last.GetComponent<HoverHighlighter>().HighlightOff();
                last = null;
            }
            last = hit.collider.gameObject;
            last.GetComponent<HoverHighlighter>().HighlightOn();
        }
    }
}
