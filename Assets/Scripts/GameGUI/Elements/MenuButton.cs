using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class MenuButton {
	public ScaledRect Position = new ScaledRect ();
	public String Text;

	public MenuButton () {
	}

	public bool Draw () {
		return GUI.Button (this.Position.getRect (), this.Text);
	}
}


