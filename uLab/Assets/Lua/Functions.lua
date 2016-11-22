

function LogInfo(text)
	Lite.Log.Info(text)
end

function LogWarning(text)
	Lite.Log.Warning(text)
end

function LogError(text)
	Lite.Log.Error(text)
end

function RegisterWindow(filePath, showMode, openAction, bgMode)
	Lite.App.uiManager.RegisterWindow(filePath, showMode, openAction, bgMode)
end

function OpenWindow(name)
	Lite.App.uiManager:OpenWindow(name)
end

function CloseWindow(name)
	Lite.App.uiManager:CloseWindow(name)
end

function SetMainWindow(name)
	Lite.App.uiManager:SetMainWindow(name)
end

function BackToMainWindow()
	Lite.App.uiManager:BackToMainWindow()
end
