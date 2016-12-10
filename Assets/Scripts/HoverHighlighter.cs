using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverHighlighter : MonoBehaviour {
    Shader old;

    void OnMouseEnter() {
        Camera mainCamera = Camera.main;
        old = GetComponent<Renderer>().material.shader;
        RaycastHit hit = new RaycastHit();
        if (
            !Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
                             mainCamera.ScreenPointToRay(Input.mousePosition).direction, out hit, 100,
                             Physics.DefaultRaycastLayers))
        {
            return;
        }
        if (hit.distance < 2f)
        {
            Renderer ren = GetComponent<Renderer>();
            if (ren)
                ren.material.shader = Shader.Find("Reflective/Parallax Diffuse");
        }
    }

    void OnMouseExit() {
        Renderer ren = GetComponent<Renderer>();
        if(ren)
            ren.material.shader = old;
    }

}
