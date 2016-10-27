
require "Defines"
require "Functions"


Game = {};
local this = Game;



function Game.OnInitialize()
	LogInfo(GameUI.bar);
	LogWarning(GameUI.bar);
	LogError(GameUI.bar);
	App.uiManager:OpenWindow(GameUI.bar);
	App.uiManager:OpenWindow(GameUI.main);
	--App.uiManager.SetMainWindow(GameUI.main);
end
