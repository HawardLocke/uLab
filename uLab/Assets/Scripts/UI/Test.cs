

using UnityEngine;

using Locke.ui;


public class GameUIList
{
	public static WindowInfo testWindow1 = new WindowInfo(
		"ui/TestWindow1",
		ShowMode.Normal,
		OpenAction.DoNothing,
		CloseAction.Traceback
		);
	public static WindowInfo testWindow2 = new WindowInfo(
		"ui/TestWindow2",
		ShowMode.Fixed,
		OpenAction.DoNothing,
		CloseAction.Traceback
		);
	public static WindowInfo testWindow3 = new WindowInfo(
		"ui/TestWindow3",
		ShowMode.Popup,
		OpenAction.DoNothing,
		CloseAction.Traceback
		);
	public static WindowInfo testWindow4 = new WindowInfo(
		"ui/TestWindow4",
		ShowMode.Normal,
		OpenAction.HideOthers,
		CloseAction.Traceback
		);
}
