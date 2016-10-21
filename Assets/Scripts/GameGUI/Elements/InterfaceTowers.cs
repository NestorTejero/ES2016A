using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InterfaceTowers {
	public InterfaceContainer Contenedor = new InterfaceContainer();

	[Header("Propiedades torres")]
	public TowerButton[] Torres; 
	public Vector2 Offset;
	public Vector2 Padding;
	public Vector2 Dimensiones;

	public InterfaceTowers () {}

	public void draw() {
		this.Contenedor.Draw ();

		ScaledRect PosicionTorre = new ScaledRect (this.Contenedor.Dimensions);
		PosicionTorre.rect.x += this.Padding.x;
		PosicionTorre.rect.y += this.Padding.y;
		PosicionTorre.rect.width = this.Dimensiones.x;
		PosicionTorre.rect.height = this.Dimensiones.y;
		for (int n = 0; n < this.Torres.Length; n++) {
			this.Torres[n].draw(new ScaledRect(PosicionTorre));
			PosicionTorre.rect.x += Offset.x;
			PosicionTorre.rect.y += Offset.y;
		}
	}
}


