using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InterfaceHealth {
	public InterfaceContainer Contenedor = new InterfaceContainer ();

	[Header("Propiedades corazones")]
	public Texture Imagen = null;
	public Vector2 Offset = new Vector2 ();
	public Vector2 Padding = new Vector2 ();
	public Vector2 Dimensiones = new Vector2 ();

	public InterfaceHealth () {}

	public void Draw() {
		this.Contenedor.Draw ();

		for (int n = 0; n < LogicConnector.getInstance().getHealth(); n++) {
			ScaledRect ItemPosition = new ScaledRect (this.Contenedor.Dimensions);
			ItemPosition.rect.x += this.Padding.x + this.Offset.x * n;
			ItemPosition.rect.y += this.Padding.y + this.Offset.y * n;
			ItemPosition.rect.width = this.Dimensiones.x;
			ItemPosition.rect.height = this.Dimensiones.y;
			GUI.DrawTexture (ItemPosition.getRect(), this.Imagen);
		}
	}
}


