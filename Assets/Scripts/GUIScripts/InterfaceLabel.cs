using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InterfaceLabel {
	public int FontSize = 24;
	public Color TextColor = new Color(0, 0, 0);
	public ScaledRect Position = new ScaledRect ();
	public String Text;

	public InterfaceLabel () {}

	public void Draw () {
		GUIStyle LabelStyle = new GUIStyle ();
		LabelStyle.fontSize = (int)(this.FontSize * Position.getXScale());
		LabelStyle.normal.textColor = this.TextColor;
		LabelStyle.alignment = TextAnchor.MiddleCenter;

		GUI.Label(this.Position.getRect(), this.Text, LabelStyle);
	}

	public void SetText(String Text) {
		this.Text = Text;
	}
}


