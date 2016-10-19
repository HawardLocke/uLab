

using Locke;
using Locke.ui;
using UnityEngine.UI;

public class BagWindow : IWindow
{
	protected override void OnEnter(IContext context)
	{
		for (int i = 0; i < 5; i++)
		{
			Button button = this.FindWidget<Button>("buttons/button" + (i + 1));
			button.transform.GetComponentInChildren<Text>().text = "button " + (i + 1);
		}

		Button back = FindWidget<Button>("buttons/back");
		back.onClick.AddListener(OnClick);
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

	private void OnClick()
	{
		this.CloseMe();
	}

}
