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
public class InterfaceTowerSelected {

	public ScaledRect Position = new ScaledRect();
    public ScaledRect PositionTitle = new ScaledRect();
    public ScaledRect PositionDamage = new ScaledRect();
    public ScaledRect PositionRange = new ScaledRect();
    public ScaledRect PositionFirerate = new ScaledRect();
    public ScaledRect PositionHealth = new ScaledRect();
    public ScaledRect PositionResultDamage = new ScaledRect();
    public ScaledRect PositionResultRange = new ScaledRect();
    public ScaledRect PositionResultFirerate = new ScaledRect();
    public ScaledRect PositionResultHealth = new ScaledRect();
    public ScaledRect PositionResultDamageUpgrade = new ScaledRect();
    public ScaledRect PositionResultRangeUpgrade = new ScaledRect();
    public ScaledRect PositionResultFirerateUpgrade = new ScaledRect();
    public ScaledRect PositionResultHealthUpgrade = new ScaledRect();
    public ScaledRect PositionNextUpgrade = new ScaledRect();
    public ScaledRect PositionResultNextUpgrade = new ScaledRect();
    public ScaledRect upgrade_button;
    public ScaledRect sell_button;

    // Information shown by the interface:
    public String TextTitle;
    public String TextDamage;
    public String TextRange;
    public String TextFirerate;
    public String TextHealth;
    public String TextNextUpgrade;
    public String ResultDamage;
    public String ResultRange;
    public String ResultFirerate;
    public String ResultHealth;
    public String ResultDamageUpgrade;
    public String ResultRangeUpgrade;
    public String ResultFirerateUpgrade;
    public String ResultHealthUpgrade;
    public String ResultNextUpgrade;
    public Texture Background;
    public int TextWidth;

    public int FontSize = 24;
    public Color TextColor = new Color(0, 0, 0);
    public TextAnchor Alignment = TextAnchor.MiddleCenter;
    public TextAnchor AlignmentStats = TextAnchor.MiddleLeft;

    public float Value = 0.0f;

    public InterfaceTowerSelected() {
	}

	public void Draw () {

        GUI.DrawTexture(this.Position.getRect(), this.Background);

        GUIStyle LabelStyle = new GUIStyle();
        GUIStyle LabelStyleStats = new GUIStyle();
        GUIStyle LabelStyleUpgrade = new GUIStyle();
        LabelStyle.fontSize = (int)(this.FontSize * Position.getXScale());
        LabelStyle.normal.textColor = this.TextColor;
        LabelStyle.alignment = this.Alignment;
        LabelStyleStats.fontSize = 20;
        LabelStyleStats.normal.textColor = this.TextColor;
        LabelStyleStats.alignment = this.AlignmentStats;
        LabelStyleUpgrade.fontSize = 16;
        LabelStyleUpgrade.normal.textColor = this.TextColor;
        LabelStyleUpgrade.alignment = this.AlignmentStats;
        TextTitle = LogicConnector.getTowerName();

        // Assign stat values
        ResultDamage = LogicConnector.getTowerDamage().ToString();
        ResultRange = LogicConnector.getTowerRange().ToString();
        ResultFirerate = LogicConnector.getTowerFirerate().ToString();
        ResultHealth = LogicConnector.getTowerHealth().ToString();

        // LABELS
        GUI.Label(this.PositionTitle.getRect(), this.TextTitle, LabelStyle);            // Damage              
        GUI.Label(this.PositionDamage.getRect(), this.TextDamage, LabelStyleStats);     // Range
        GUI.Label(this.PositionRange.getRect(), this.TextRange, LabelStyleStats);       // Firerate
        GUI.Label(this.PositionFirerate.getRect(), this.TextFirerate, LabelStyleStats); // Health
        GUI.Label(this.PositionHealth.getRect(), this.TextHealth, LabelStyleStats);     // Upgrade

        // STAT VALUES
        GUI.Label(this.PositionResultDamage.getRect(), this.ResultDamage, LabelStyleStats);     // Damage
        GUI.Label(this.PositionResultRange.getRect(), this.ResultRange, LabelStyleStats);       // Range
        GUI.Label(this.PositionResultFirerate.getRect(), this.ResultFirerate, LabelStyleStats); // Firerate
        GUI.Label(this.PositionResultHealth.getRect(), this.ResultHealth, LabelStyleStats);     // Health
        GUI.Label(this.PositionNextUpgrade.getRect(), this.TextNextUpgrade, LabelStyleUpgrade); // Upgrade

        // DAMAGE UPGRADE
        if (LogicConnector.getTowerDamageUpgrade() > LogicConnector.getTowerDamage())
        {
            ResultDamageUpgrade = "(+" + (LogicConnector.getTowerDamageUpgrade() - LogicConnector.getTowerDamage()).ToString() + ")";
            LabelStyleUpgrade.normal.textColor = new Color(0f, 0.4f, 0f);
        }
        else
        {
            ResultDamageUpgrade = "(-)";
            LabelStyleUpgrade.normal.textColor = new Color(0.4f, 0.4f, 0.4f);
        }   
        GUI.Label(this.PositionResultDamageUpgrade.getRect(), this.ResultDamageUpgrade, LabelStyleUpgrade);

        // RANGE UPGRADE
        if (LogicConnector.getTowerRangeUpgrade() > LogicConnector.getTowerRange())
        {
            ResultRangeUpgrade = "(+" + (LogicConnector.getTowerRangeUpgrade() - LogicConnector.getTowerRange()).ToString() + ")";
            LabelStyleUpgrade.normal.textColor = new Color(0f, 0.4f, 0f);
        }
        else
        {
            ResultRangeUpgrade = "(-)";
            LabelStyleUpgrade.normal.textColor = new Color(0.4f, 0.4f, 0.4f);
        }
        GUI.Label(this.PositionResultRangeUpgrade.getRect(), this.ResultRangeUpgrade, LabelStyleUpgrade);

        // FIRERATE UPGRADE
        if (LogicConnector.getTowerFirerate() > LogicConnector.getTowerFirerateUpgrade())
        {
            ResultFirerateUpgrade = "(-" + (LogicConnector.getTowerFirerate() - LogicConnector.getTowerFirerateUpgrade()).ToString() + ")";
            LabelStyleUpgrade.normal.textColor = new Color(0f, 0.4f, 0f);
        }
        else
        {
            ResultFirerateUpgrade = "(-)";
            LabelStyleUpgrade.normal.textColor = new Color(0.4f, 0.4f, 0.4f);
        }
        GUI.Label(this.PositionResultFirerateUpgrade.getRect(), this.ResultFirerateUpgrade, LabelStyleUpgrade);

        // HEALTH UPGRADE
        if (LogicConnector.getTowerHealthUpgrade() > LogicConnector.getTowerHealth())
        {
            ResultHealthUpgrade = "(+" + (LogicConnector.getTowerHealthUpgrade() - LogicConnector.getTowerHealth()).ToString() + ")";
            LabelStyleUpgrade.normal.textColor = new Color(0f, 0.4f, 0f);
        }
        else
        {
            ResultHealthUpgrade = "(-)";
            LabelStyleUpgrade.normal.textColor = new Color(0.4f, 0.4f, 0.4f);
        }
        GUI.Label(this.PositionResultHealthUpgrade.getRect(), this.ResultHealthUpgrade, LabelStyleUpgrade);

        // UPGRADE COST
        if (LogicConnector.getTowerCostUpgrade() > 0)
        {
            ResultNextUpgrade = "- "+LogicConnector.getTowerCostUpgrade().ToString();
            LabelStyleUpgrade.normal.textColor = new Color(0.4f, 0f, 0f);
        }
        else
        {
            ResultNextUpgrade = " - ";
            LabelStyleUpgrade.normal.textColor = new Color(0.4f, 0.4f, 0.4f);
        }
        GUI.Label(this.PositionResultNextUpgrade.getRect(), this.ResultNextUpgrade, LabelStyleUpgrade);

        // BUTTONS
        GUI.Button(this.upgrade_button.getRect(), "UPGRADE");
        GUI.Button(this.sell_button.getRect(), "SELL");

        LogicConnector.setRectUpgrade(upgrade_button.getRect());
        LogicConnector.setRectSell(sell_button.getRect());

    }
}


