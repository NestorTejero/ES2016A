/* --------------------------------------------------------------------------------------------	*
 * README: 	Este script es parte del codigo del equipo C (User interface - UX), por favor		*
 * 			no realizes cambios en este archivo si no eres su autor, de su grupo o has			*
 * 			hablado con alguno de ellos para comentar los cambios y se ha hecho un acuerdo.		*
 * 			Anteriormente se han realizado cambios en archivos del equipo C por partes de		*
 * 			otros equipos resultando en una duplicación de las funcionalidades y un uso			*
 * 			incorrecto de variables que estaban destinadas a test internos de la interfaz.		*
 * 			Para evitar tener que hacer el trabajo repetidas veces por favor habla con			*
 * 			los autores, intentemos no pisarnos el trabajo para poder avanzar todos mejor.		*
 * --------------------------------------------------------------------------------------------	*/
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


