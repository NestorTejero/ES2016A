using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class TowerButton {
	public Texture Image;

	public TowerButton () {}

	public void draw(ScaledRect Position) {
		GUI.DrawTexture(Position.getRect(), this.Image);
	}
}


