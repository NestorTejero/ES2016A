using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InterfaceHealth {
	private Rect WindowSize;

	[Header("Propiedades contenedor")]
	public ScaledRect Position 		= new ScaledRect ();
	public Texture 	Background 		= null;
	[Header("Propiedades corazones")]
	public int 		Count 			= 3;
	public Vector2 	HeartDimension 	= new Vector2 ();
	public Vector2 	HeartOffset 	= new Vector2 ();
	public Vector2	HeartPadding 	= new Vector2 ();
	public Texture 	HeartImage 		= null;


	public InterfaceHealth () {}

	public void draw() {
		GUI.DrawTexture (this.Position.getRect(), this.Background);
		for (int n = 0; n < this.Count; n++) {
			ScaledRect ItemPosition = new ScaledRect (this.Position);
			ItemPosition.rect.x += this.HeartPadding.x + this.HeartOffset.x * n;
			ItemPosition.rect.y += this.HeartPadding.y + this.HeartOffset.y + n;
			ItemPosition.rect.width = this.HeartDimension.x;
			ItemPosition.rect.height = this.HeartDimension.y;
			GUI.DrawTexture (ItemPosition.getRect(), this.HeartImage);
		}
	}

	public void setWindowSize(Rect WindowSize) {
		this.WindowSize = WindowSize;
		this.Position.setWindowSize (WindowSize);
	}
}


