﻿using UnityEngine;
using System.Collections;

public class Crosshairs : MonoBehaviour {

	public LayerMask targetMask;
	public SpriteRenderer dot;
	public Color dotHighlightColor;
	Color originalDotColor;

	void Start(){
		Cursor.visible = false;
		originalDotColor = dot.color;
	}
		
	void Update () {
	}

	public void DetectTargets(Ray ray)
	{
		if (Physics.Raycast (ray, 100, targetMask)) {
			dot.color = dotHighlightColor;
		} else {
			dot.color = originalDotColor;
		}
	}
}
