using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InterfaceTowers {
	[Header("Propiedades contenedor")]
	public ScaledRect Position 		= new ScaledRect ();
	public Texture 	Background 		= null;
	[Header("Propiedades torres")]
	public TowerButton[] Torres; 
	public Vector2 Offset;
	public Vector2 Padding;
	public Vector2 ButtonSize;

	public InterfaceTowers () {
	}

	public void draw() {
		GUI.DrawTexture (this.Position.getRect (), this.Background);

		ScaledRect PosicionTorre = new ScaledRect (this.Position);
		PosicionTorre.rect.x += this.Padding.x;
		PosicionTorre.rect.y += this.Padding.y;
		PosicionTorre.rect.width = this.ButtonSize.x;
		PosicionTorre.rect.height = this.ButtonSize.y;
		for (int n = 0; n < this.Torres.Length; n++) {
			this.Torres[n].draw(new ScaledRect(PosicionTorre));
			PosicionTorre.rect.x += Offset.x;
			PosicionTorre.rect.y += Offset.y;
		}
	}

	public void setWindowSize(Rect WindowSize) {
		this.Position.setWindowSize (WindowSize);
	}
}


