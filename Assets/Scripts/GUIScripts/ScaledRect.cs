using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class ScaledRect {
	public static Rect WindowSize;
	public Rect rect;

	public ScaledRect() {}

	public ScaledRect(ScaledRect source) {
		this.rect = new Rect(source.rect);
	}

	public float getXScale() {
		return Screen.width / ScaledRect.WindowSize.width;
	}

	public float getYScale() {
		return Screen.height / ScaledRect.WindowSize.height;
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

	public void IncrementPosition(ScaledRect source) {
		this.rect.x += source.rect.x;
		this.rect.y += source.rect.y;
	}
}

