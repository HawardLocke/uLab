

using Locke;
using Locke.ui;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : IWindow
{
	protected override void OnEnter(IContext context)
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
		this.CloseMe();
	}

	private void OnBackClick()
	{
		this.CloseMe();
	}

}
