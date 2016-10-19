

using Locke;
using Locke.ui;
using UnityEngine.UI;

public class BarWindow : IWindow
{
	protected override void OnEnter(IContext context)
	{
		Button button = this.FindWidget<Button>("buttons/button1");
		button.transform.GetComponentInChildren<Text>().text = "I'm bar.";
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

}
