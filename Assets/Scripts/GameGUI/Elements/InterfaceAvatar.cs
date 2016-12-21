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
public class InterfaceAvatar {
	public InterfaceContainer Contenedor = new InterfaceContainer ();

	[Header("Propiedades avatar")]
	public Texture Avatar = null;
	public ScaledRect DimensionesAvatar = new ScaledRect();

	public InterfaceAvatar () {}

	public void Draw() {
		this.Contenedor.Draw ();

		ScaledRect AvatarPosition = new ScaledRect (this.DimensionesAvatar);
		AvatarPosition.IncrementPosition (this.Contenedor.Dimensions);
		GUI.DrawTexture (AvatarPosition.getRect (), this.Avatar);
	}
}

