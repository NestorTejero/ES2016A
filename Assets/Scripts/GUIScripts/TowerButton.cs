using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class TowerButton {
	public Texture Background;
	public Texture BackgroundHover;
	public Texture BackgroundClick;
	public Texture Image;
	public Texture ImageLocked;
	public Boolean Locked;
	public Boolean ShowCost = true;
	public int type;

	public TowerButton () {}

	public void draw(ScaledRect Position) {
		if(this.Background != null)
			GUI.DrawTexture(Position.getRect(), this.Background);

		int TowerCost = LogicConnector.getInstance ().getTowerCost (this.type);


		if (this.Locked || TowerCost > LogicConnector.getInstance().getCredit()) {
			GUI.DrawTexture(Position.getRect(), this.ImageLocked);
		} else {
			GUIStyle buttonStyle = new GUIStyle();
			buttonStyle.normal.background = this.Background as Texture2D;
			buttonStyle.hover.background = this.BackgroundHover as Texture2D;
			buttonStyle.active.background = this.BackgroundClick as Texture2D;

			if (GUI.Button (Position.getRect (), this.Image, buttonStyle)) {
				LogicConnector.getInstance ().placeTower (this.type);
			}
		}

		if (this.ShowCost) {
			ScaledRect LabelPosition = new ScaledRect (Position);
			LabelPosition.rect.x += 5;
			LabelPosition.rect.y += (float)(LabelPosition.rect.height - 24);

			GUIStyle LabelStyle = new GUIStyle ();
			LabelStyle.fontSize = 24;
			LabelStyle.normal.textColor = new Color (0, 0, 0);

			GUI.Label (LabelPosition.getRect (), TowerCost.ToString (), LabelStyle);
		}

	}
}


