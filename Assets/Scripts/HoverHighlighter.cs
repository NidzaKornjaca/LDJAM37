using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverHighlighter : MonoBehaviour {
    Shader old;

    void OnMouseEnter() {
        old = GetComponent<Renderer>().material.shader;
        GetComponent<Renderer>().material.shader = Shader.Find("Tessellation/Bumped Specular (smooth)");
    }

    void OnMouseExit() {
        GetComponent<Renderer>().material.shader = old;
    }

}
