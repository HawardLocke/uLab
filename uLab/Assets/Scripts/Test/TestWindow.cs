
using UnityEngine;
using UnityEngine.UI;

using Locke;
using Locke.ui;


public class TestWindow : IWindow
{
	protected override void OnEnter(IContext context)
	{
		for (int i = 0; i < 5; i++)
		{
			Button button = this.FindWidget<Button>("buttons/button" + (i + 1));
			button.transform.GetComponentInChildren<Text>().text = "button " + (i + 1);
			UIEventListener.Get(button.gameObject).onClick = OnButtonClick;
		}

		Button back = FindWidget<Button>("buttons/back");
		UIEventListener.Get(back.gameObject).onClick = OnBackClick;
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

	private void OnButtonClick(GameObject go)
	{
		int index = int.Parse(go.name.Substring(go.name.Length - 1));
		Log.Info("to t" + index);
		if (index == 1)
		{
			// to t2
		}
		else if (index == 2)
		{
			// to t3
		}
		else if (index == 3)
		{
			// to t4
		}
		else if (index == 4)
		{
			// to t2
		}
		else if (index == 5)
		{

		}
	}

	private void OnBackClick(GameObject go)
	{
		Log.Info("on click");
		UIManager.Instance.CloseCurrentWindow();
	}

}
