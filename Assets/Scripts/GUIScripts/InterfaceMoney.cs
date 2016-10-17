using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InterfaceMoney {
	private Rect WindowSize;

	[Header("Propiedades contenedor")]
	public ScaledRect Position 		= new ScaledRect ();
	public Texture 	Background 		= null;
	[Header("Propiedades dinero")]
	public int 		Count 			= 3;
	public Vector2  DineroPadding   = new Vector2();
	public GUIStyle TextStyle = new GUIStyle ();

	public InterfaceMoney () {
	}

	public void draw() {
		if(this.Background != null)
			GUI.DrawTexture (this.Position.getRect(), this.Background);

		ScaledRect positionText = new ScaledRect (this.Position);
		positionText.rect.x += this.DineroPadding.x;
		positionText.rect.y += this.DineroPadding.y;
		GUI.Label (positionText.getRect(), LogicConnector.getInstance().getCredit().ToString(), this.TextStyle);
	}

	public void setWindowSize(Rect WindowSize) {
		this.WindowSize = WindowSize;
		this.Position.setWindowSize (WindowSize);
	}
}


