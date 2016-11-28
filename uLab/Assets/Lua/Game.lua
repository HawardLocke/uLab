
require "3rd/pbc/protobuf"

require "Common/functions"
require "UI/UIBase"

require "Defines"
require "Functions"
require "Network"
require "UI/UIDefines"

require "protocol/MsgID"

--require "protocol/login_pb"
--require "protocol/scene_pb"

require "MsgHandlers/LoginHandler"
require "MsgHandlers/SceneHandler"


Game = class("Game")
local this = Game


function Game:OnInitialize()
	this.RegisterPBC()
	this.InitMsgHandlers()
	OpenWindow(GameUI.login)
	SetMainWindow(GameUI.login)

	App.networkManager:SendConnect()

end


function Game:InitMsgHandlers()
	LoginHandler:Register()
	SceneHandler:Register()
end

function Game:RegisterPBC()
    local addr = io.open('D:/Locke/u/GitHub/uLab/uLab/Assets/Lua/'.."protocol/login.pb", "rb")
    local buffer = addr:read "*a"
    addr:close()
    protobuf.register(buffer)

    addr = io.open('D:/Locke/u/GitHub/uLab/uLab/Assets/Lua/'.."protocol/scene.pb", "rb")
    buffer = addr:read "*a"
    addr:close()
    protobuf.register(buffer)
end











