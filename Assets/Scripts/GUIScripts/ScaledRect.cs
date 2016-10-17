using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class ScaledRect {
	private Rect WindowSize;
	public  Rect rect;

	public ScaledRect() {

	}

	public ScaledRect (Rect WindowSize) {
		this.WindowSize = WindowSize;
	}

	public ScaledRect(ScaledRect source) {
		this.WindowSize = source.WindowSize;
		this.rect = new Rect(source.rect);
	}

	public void setWindowSize(Rect WindowSize) {
		this.WindowSize = WindowSize;
	}

	public float getXScale() {
		return Screen.width / WindowSize.width;
	}

	public float getYScale() {
		return Screen.height / WindowSize.height;
	}
		
	public Rect getRect() {
		Rect scaled = new Rect (this.rect);
		float xScale = this.getXScale ();
		float yScale = this.getYScale ();
		scaled.x *= xScale;
		scaled.y *= yScale;
		scaled.width *= xScale;
		scaled.height *= yScale;
		return scaled;
	}
}

