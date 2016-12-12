using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

[Serializable]
public class InterfaceGameOver {
	public InterfaceContainer Contenedor = new InterfaceContainer();
	public InterfaceContainer ContenedorBlur = new InterfaceContainer();

	[Header("Estadisticas")]
	public InterfaceLabel TituloRondas = new InterfaceLabel ();
	public InterfaceLabel VariableRondas = new InterfaceLabel ();
	public InterfaceLabel TituloEnemigos = new InterfaceLabel ();
	public InterfaceLabel VariableEnemigos = new InterfaceLabel ();
	public InterfaceLabel TituloTorresCompradas = new InterfaceLabel ();
	public InterfaceLabel VariableTorresCompradas = new InterfaceLabel ();
	public InterfaceLabel TituloTorresVendidas = new InterfaceLabel ();
	public InterfaceLabel VariableTorresVendidas = new InterfaceLabel ();
	public InterfaceLabel TituloDinero = new InterfaceLabel ();
	public InterfaceLabel VariableDinero = new InterfaceLabel ();
	public InterfaceLabel TituloPuntuacion = new InterfaceLabel ();
	public InterfaceLabel VariablePuntuacion = new InterfaceLabel ();

	[Header("Controles")]
	public MenuButton BotonMenuPrincipal = new MenuButton ();
	public MenuButton BotonSalir = new MenuButton ();

	public InterfaceGameOver () {}

	public void Draw () {
		if (LogicConnector.isGameOver ()) {
			// Update stats
			GameObject go = GameObject.Find("GameScripts");
			Score score = go.GetComponent (typeof(Score)) as Score;
			VariableRondas.SetText (score.getRound ().ToString());
			VariableEnemigos.SetText (score.getEnemies ().ToString());
			VariableTorresCompradas.SetText (score.getTowersBuilt ().ToString());
			VariableTorresVendidas.SetText (score.getTowersSold ().ToString());
			VariableDinero.SetText (score.getGoldEarned ().ToString());
			VariablePuntuacion.SetText (score.getScore ().ToString());

			TituloRondas.Draw ();
			VariableRondas.Draw ();
			TituloEnemigos.Draw ();
			VariableEnemigos.Draw ();
			TituloTorresCompradas.Draw ();
			VariableTorresCompradas.Draw ();
			TituloTorresVendidas.Draw ();
			VariableTorresVendidas.Draw ();
			TituloDinero.Draw ();
			VariableDinero.Draw ();
			TituloPuntuacion.Draw ();
			VariablePuntuacion.Draw ();

			if (BotonMenuPrincipal.Draw ()) {
				SceneManager.LoadScene ("menuScene");
			}

			if (BotonSalir.Draw ()) {
				Application.Quit ();
			}
		}
	}
}


