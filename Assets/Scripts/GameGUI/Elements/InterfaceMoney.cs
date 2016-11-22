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
public class InterfaceMoney {
	public InterfaceContainer Contenedor = new InterfaceContainer ();
	public InterfaceLabel Label = new InterfaceLabel();

	public InterfaceMoney () {}

    #region Variables actualización money

    public InterfaceContainer MoneyUpContainer = new InterfaceContainer();

    
    //public Texture TexturaMoneyUp;
    //public ScaledRect PosiTionMoneyUp;
    //public int MoneyUpTime;

    private float MoneyUpDeltaTime = 1.0f;
    private float LastDeltaTime = 0f;
    private int LastMoney = -1;
    private bool ShowingCoinUp = false;
    #endregion

    public void Draw() {
		this.Contenedor.Draw ();

		this.Label.SetText (LogicConnector.getCredit().ToString ());
		this.Label.Draw ();


        if (LastMoney == -1)
            LastMoney = LogicConnector.getCredit();

        ShowCounUp();
        
    }
    public void ShowCounUp()
    {
        var credit_now = LogicConnector.getCredit();
        if (LastMoney > credit_now)
        {
            if (!ShowingCoinUp)
            {
                ShowingCoinUp = true;
                LastDeltaTime = Time.realtimeSinceStartup;
            }
            else
            {
                if (LastDeltaTime + MoneyUpDeltaTime < Time.realtimeSinceStartup)
                {
                    ShowingCoinUp = false;
                    LastMoney = credit_now;
                }
            }
            if (ShowingCoinUp)
            {
                MoneyUpContainer.Draw();
            }
        }
    }
}


