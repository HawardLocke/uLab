

function LogInfo(text)
	Locke.Log.Info(text);
end

function LogWarning(text)
	Locke.Log.Warning(text);
end

function LogError(text)
	Locke.Log.Error(text);
end

function RegisterWindow(filePath, showMode, openAction, bgMode)
	Locke.App.uiManager.RegisterWindow(filePath, showMode, openAction, bgMode);
end

function OpenWindow(name)
	Locke.App.uiManager:OpenWindow(name);
end

function CloseWindow(name)
	Locke.App.uiManager:CloseWindow(name);
end

function SetMainWindow(name)
	Locke.App.uiManager:SetMainWindow(name);
end

function BackToMainWindow()
	Locke.App.uiManager:BackToMainWindow();
end
