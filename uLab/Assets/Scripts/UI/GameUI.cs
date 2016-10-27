

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Locke.ui;


public class GameUI
{
	// for flexible access.
	private static Dictionary<string, WindowInfo> mWindowInfoMap = new Dictionary<string, WindowInfo>();
	public static WindowInfo GetWindowInfo(string windowName)
	{
		WindowInfo info = null;
		mWindowInfoMap.TryGetValue(windowName, out info);
		return info;
	}
	private static WindowInfo AddWindowInfo(string path, ShowMode showMode, OpenAction openAct, BackgroundMode bgMode)
	{
		var info = new WindowInfo(path, showMode, openAct, bgMode);
		mWindowInfoMap.Add(info.name, info);
		return info;
	}

	public static WindowInfo main = AddWindowInfo(
		"ui/MainWindow",
		ShowMode.Main,
		OpenAction.DoNothing,
		BackgroundMode.None
		);
	public static WindowInfo bar = AddWindowInfo(
		"ui/BarWindow",
		ShowMode.Fixed,
		OpenAction.DoNothing,
		BackgroundMode.None
		);
	public static WindowInfo role = AddWindowInfo(
		"ui/RoleWindow",
		ShowMode.Normal,
		OpenAction.HideNormalsMains,
		BackgroundMode.Transparent
		);
	public static WindowInfo shop = AddWindowInfo(
		"ui/ShopWindow",
		ShowMode.Normal,
		OpenAction.HideNormalsMains,
		BackgroundMode.Transparent
		);
	public static WindowInfo bag = AddWindowInfo(
		"ui/BagWindow",
		ShowMode.Normal,
		OpenAction.HideNormalsMains,
		BackgroundMode.Transparent
		);
	public static WindowInfo tip = AddWindowInfo(
		"ui/TipWindow",
		ShowMode.Popup,
		OpenAction.DoNothing,
		BackgroundMode.Dark
		);
	public static WindowInfo dialog = AddWindowInfo(
		"ui/DialogWindow",
		ShowMode.Normal,
		OpenAction.HideAll,
		BackgroundMode.Dark
		);


}
