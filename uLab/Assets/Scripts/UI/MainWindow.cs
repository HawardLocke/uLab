
using UnityEngine;
using UnityEngine.UI;

using Locke;
using Locke.ui;


public class MainWindow : IWindow
{
	private static string[] buttonNames = new string[9] { "bag", "role", "shop", "tip", "dialog", "close normals", "close all", "show main", "reset" };
	protected override void OnInit()
	{
		for (int i = 0; i < 9; i++)
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
			App.uiManager.OpenWindow(GameUI.bag);
		}
		else if (index == 2)
		{
			App.uiManager.OpenWindow(GameUI.role);
		}
		else if (index == 3)
		{
			App.uiManager.OpenWindow(GameUI.shop);
		}
		else if (index == 4)
		{
			App.uiManager.OpenWindow(GameUI.tip);
		}
		else if (index == 5)
		{
			App.uiManager.OpenWindow(GameUI.dialog);
		}
		else if (index == 6)
		{
			App.uiManager.BackToMainWindow();
		}
		else if (index == 7)
		{
			//UIManager.Instance.CloseAllWindows();
		}
		else if (index == 8)
		{
			//UIManager.Instance.CloseAllWindowsExcept(GameUIList.main);
		}
		else if (index == 9)
		{
			App.uiManager.Cleanup();
		}
	}

}
