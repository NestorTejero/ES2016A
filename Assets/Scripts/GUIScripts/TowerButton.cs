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
			InterfaceLabel Label = new InterfaceLabel ();
			Label.Position = new ScaledRect (Position);
			Label.Position.rect.y += (int)(2 * (Position.rect.height / 3.0));
			Label.Position.rect.height = (int)(Position.rect.height / 3.0);
			Label.FontSize = 24;
			Label.SetText (TowerCost.ToString ());
			Label.Draw ();
		}

	}
}


