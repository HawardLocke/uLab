

using Locke;
using Locke.ui;
using UnityEngine;
using UnityEngine.UI;

public class TipWindow : IWindow
{
	protected override void OnInit()
	{
		Button button = this.FindWidget<Button>("buttons/button1");
		UIEventListener.Get(button.gameObject).onClick = onOkClick;

		Button back = FindWidget<Button>("buttons/back");
		back.onClick.AddListener(OnBackClick);
	}

	protected override void OnExit(IContext context)
	{
	}

	protected override void OnPause(IContext context)
	{
	}

	protected override void OnResume(IContext context)
	{
	}

	private void onOkClick(GameObject go)
	{
		App.uiManager.CloseWindow(this);
	}

	private void OnBackClick()
	{
		App.uiManager.CloseWindow(this);
	}

}
