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
public class ScaledRect {
	public static Rect WindowSize;
	public Rect rect;

	public ScaledRect() {}

	public ScaledRect(ScaledRect source) {
		this.rect = new Rect(source.rect);
	}

	public float getXScale() {
		return Screen.width / ScaledRect.WindowSize.width;
	}

	public float getYScale() {
		return Screen.height / ScaledRect.WindowSize.height;
	}
		
	public Rect getRect() {
		Rect scaled = new Rect (this.rect);
		float xScale = this.getXScale ();
		float yScale = this.getYScale ();
		scaled.x *= xScale;
		scaled.y *= yScale;
		scaled.width *= xScale;
		scaled.height *= yScale;
		return scaled;
	}

	public void IncrementPosition(ScaledRect source) {
		this.rect.x += source.rect.x;
		this.rect.y += source.rect.y;
	}
}

