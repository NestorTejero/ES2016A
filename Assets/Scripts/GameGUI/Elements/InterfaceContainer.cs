using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InterfaceContainer {
	public ScaledRect Dimensions = new ScaledRect();
	public Texture Background;

	public InterfaceContainer () {}

	public void Draw() {
		if(this.Background != null)
			GUI.DrawTexture (this.Dimensions.getRect(), this.Background);
	}
}


