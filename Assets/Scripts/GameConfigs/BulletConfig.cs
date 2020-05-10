using UnityEngine;
using System.Collections;

public partial class BulletConfig : GameConfigDataBase
{
	public string id;
	public string bulletName;
	public int killValue;
	public int speed;
	public int coinCost;
	protected override string getFilePath ()
	{
		return "BulletConfig";
	}
}
