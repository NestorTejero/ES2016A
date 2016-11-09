using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InterfaceSlider {
	public ScaledRect Position = new ScaledRect();
	public String Text;
	public int TextWidth;
	public GUIStyle SliderStyle = new GUIStyle();
	public GUIStyle ThumbStyle = new GUIStyle ();

	public InterfaceLabel Label = new InterfaceLabel();
	public float Value = 0.0f;

	public InterfaceSlider () {
	}

	public float Draw () {

		ScaledRect TextPosition = new ScaledRect (this.Position);
		TextPosition.rect.width = TextWidth;

		this.Label.Position = TextPosition;
		this.Label.Alignment = TextAnchor.MiddleLeft;
		this.Label.Text = this.Text;

		this.Label.Draw ();

		ScaledRect FinalPosition = new ScaledRect (this.Position);
		FinalPosition.rect.x += this.TextWidth;
		FinalPosition.rect.width -= this.TextWidth;

		this.Value = GUI.HorizontalSlider (FinalPosition.getRect (), this.Value, 0.0f, 1.0f);
		return this.Value;
	}
}


