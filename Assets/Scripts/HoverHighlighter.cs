﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverHighlighter : MonoBehaviour {
    Shader old;

    public void HighlightOn() {
        old = GetComponent<Renderer>().material.shader;
        Renderer ren = GetComponent<Renderer>();
        if (ren)
            ren.material.shader = Shader.Find("Reflective/Parallax Diffuse");
    }

    public void HighlightOff() {
        Renderer ren = GetComponent<Renderer>();
        if (ren)
            ren.material.shader = old;
    }

}
