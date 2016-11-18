

require "Defines"
require "Functions"
require "Network"
require "UI/UIDefines"

require "protocol/MsgID"
require "protocol/login_pb"
require "protocol/scene_pb"

require "MsgHandlers/LoginHandler"
require "MsgHandlers/SceneHandler"


Game = {};
local this = Game;


function Game.OnInitialize()
	OpenWindow(GameUI.login);
	SetMainWindow(GameUI.login);

	this.InitMsgHandlers();

	App.networkManager:SendConnect();

end


function Game.InitMsgHandlers()
	LoginHandler.Register();
	SceneHandler.Register();
end












