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

		for (int n = 0; n < LogicConnector.getHealth(); n++) {
			ScaledRect ItemPosition = new ScaledRect (this.Contenedor.Dimensions);
			ItemPosition.rect.x += this.Padding.x + this.Offset.x * n;
			ItemPosition.rect.y += this.Padding.y + this.Offset.y * n;
			ItemPosition.rect.width = this.Dimensiones.x;
			ItemPosition.rect.height = this.Dimensiones.y;
			GUI.DrawTexture (ItemPosition.getRect(), this.Imagen);
		}
	}
}


