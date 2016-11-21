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
using UnityEngine.SceneManagement;

[Serializable]
public class InterfacePause {
	public InterfaceContainer Contenedor = new InterfaceContainer();
	public InterfaceContainer ContenedorBlur = new InterfaceContainer();

	public InterfaceLabel MenuTitle = new InterfaceLabel ();

	[Header("Opciones menu")]
	public MenuButton BotonContinuar = new MenuButton ();
	public MenuButton BotonOpciones = new MenuButton ();
	public MenuButton BotonReturn = new MenuButton ();


	public InterfacePause () {}

    private menusScript MenuPrincipalScript;

    public void Draw () {
		if (InterfaceState.isPaused ()) {
			this.ContenedorBlur.Draw ();
			this.Contenedor.Draw ();
			this.MenuTitle.Draw ();

			if (this.BotonContinuar.Draw ()) {
				InterfaceState.Resume ();
			}
			if (this.BotonOpciones.Draw ()) {
				InterfaceState.Settings ();
			}
			if (this.BotonReturn.Draw ()) {
                //Scene sc = SceneManager.GetActiveScene();
                //int numSceneActive = sc.buildIndex;
                
                SceneManager.LoadScene("Menu");
                //Application.Quit ();
			}
		}
	}
}


