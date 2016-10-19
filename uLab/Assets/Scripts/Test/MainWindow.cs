
using UnityEngine;
using UnityEngine.UI;

using Locke;
using Locke.ui;


public class MainWindow : IWindow
{
	private static string[] buttonNames = new string[5] { "bag", "role", "shop", "tip", "dialog" };
	protected override void OnEnter(IContext context)
	{
		for (int i = 0; i < 5; i++)
		{
			Button button = this.FindWidget<Button>("buttons/button" + (i + 1));
			button.transform.GetComponentInChildren<Text>().text = buttonNames[i];
			UIEventListener.Get(button.gameObject).onClick = OnButtonClick;
		}
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
		
		if (index == 1)
		{
			UIManager.Instance.ShowWindow(GameUIList.bag);
		}
		else if (index == 2)
		{
			UIManager.Instance.ShowWindow(GameUIList.role);
		}
		else if (index == 3)
		{
			UIManager.Instance.ShowWindow(GameUIList.shop);
		}
		else if (index == 4)
		{
			UIManager.Instance.ShowWindow(GameUIList.tip);
		}
		else if (index == 5)
		{
			UIManager.Instance.ShowWindow(GameUIList.dialog);
		}
	}

}
