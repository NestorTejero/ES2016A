using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InterfaceAvatar {
	[Header("Propiedades contenedor")]
	public ScaledRect Position 		= new ScaledRect ();
	public Texture 	Background 		= null;
	[Header("Propiedades avatar")]
	public Texture Avatar = null;
	public Vector2 Offset = new Vector2();
	public Vector2 AvatarSize = new Vector2 ();

	public InterfaceAvatar () {}

	public void draw() {
		if(this.Background != null)
			GUI.DrawTexture (this.Position.getRect (), this.Background);

		ScaledRect PositionAvatar = new ScaledRect (this.Position);
		PositionAvatar.rect.x += Offset.x;
		PositionAvatar.rect.y += Offset.y;
		PositionAvatar.rect.width = this.AvatarSize.x;
		PositionAvatar.rect.height = this.AvatarSize.y;
		GUI.DrawTexture (PositionAvatar.getRect (), this.Avatar);
	}

	public void setWindowSize(Rect WindowSize) {
		this.Position.setWindowSize (WindowSize);
	}


}

