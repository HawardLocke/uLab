

using UnityEngine;

using Locke.ui;


public class GameUIList
{
	public static WindowInfo main = new WindowInfo(
		"ui/main", 
		ShowMode.Normal, 
		OpenAction.DoNothing, 
		CloseAction.Traceback
		);
	public static WindowInfo topbar = new WindowInfo(
		"ui/topbar", 
		ShowMode.Fixed, 
		OpenAction.DoNothing, 
		CloseAction.Traceback
		);
	public static WindowInfo tip = new WindowInfo(
		"ui/tip", 
		ShowMode.Popup, 
		OpenAction.DoNothing, 
		CloseAction.Traceback
		);
	public static WindowInfo bag = new WindowInfo(
		"ui/bag", 
		ShowMode.Normal, 
		OpenAction.HideOthers, 
		CloseAction.Traceback
		);
}
