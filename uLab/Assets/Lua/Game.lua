
require "Defines"
require "Functions"
require "UI/UIDefines"


Game = {};
local this = Game;



function Game.OnInitialize()
	--LogInfo(GameUI.bar);
	--LogWarning(GameUI.bar);
	--LogError(GameUI.bar);
	OpenWindow(GameUI.bar);
	OpenWindow(GameUI.main);
	SetMainWindow(GameUI.main);
	--App.uiManager.SetMainWindow(GameUI.main);
end
