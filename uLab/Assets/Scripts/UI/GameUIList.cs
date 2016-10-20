

using UnityEngine;

using Locke.ui;


public class GameUIList
{
	public static WindowInfo main = new WindowInfo(
		"ui/MainWindow",
		ShowMode.Normal,
		OpenAction.DoNothing,
		BackgroundMode.None
		);
	public static WindowInfo bar = new WindowInfo(
		"ui/BarWindow",
		ShowMode.Fixed,
		OpenAction.DoNothing,
		BackgroundMode.None
		);
	public static WindowInfo role = new WindowInfo(
		"ui/RoleWindow",
		ShowMode.Normal,
		OpenAction.HideNormals,
		BackgroundMode.Transparent
		);
	public static WindowInfo shop = new WindowInfo(
		"ui/ShopWindow",
		ShowMode.Normal,
		OpenAction.HideNormals,
		BackgroundMode.Transparent
		);
	public static WindowInfo bag = new WindowInfo(
		"ui/BagWindow",
		ShowMode.Normal,
		OpenAction.HideNormals,
		BackgroundMode.Transparent
		);
	public static WindowInfo tip = new WindowInfo(
		"ui/TipWindow",
		ShowMode.Popup,
		OpenAction.DoNothing,
		BackgroundMode.Dark
		);
	public static WindowInfo dialog = new WindowInfo(
		"ui/DialogWindow",
		ShowMode.Normal,
		OpenAction.HideAll,
		BackgroundMode.Dark
		);
}
