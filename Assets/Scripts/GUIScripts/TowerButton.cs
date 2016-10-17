using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class TowerButton {
	public Texture Background;
	public Texture Image;
	public Texture ImageLocked;
	public Boolean Locked;

	public TowerButton () {}

	public void draw(ScaledRect Position) {
		GUI.DrawTexture(Position.getRect(), this.Background);
		if (this.Locked) {
			GUI.DrawTexture(Position.getRect(), this.ImageLocked);
		} else {
			GUI.DrawTexture(Position.getRect(), this.Image);
		}

	}
}


