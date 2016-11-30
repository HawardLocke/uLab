
require "3rd/pbc/protobuf"

require "Common/functions"
require "UI/UIBase"

require "Defines"
require "Functions"
require "Common/functions"
require "Network"
require "UI/UIDefines"
require "UI/UIBase"
require "UI/UIManager"
require "protocol/MsgID"


require "MsgHandlers/LoginHandler"
require "MsgHandlers/SceneHandler"


Game = class("Game")


function Game:OnInitialize()
	self.RegisterPBC()
	self.InitMsgHandlers()
	gUIManager:OpenWindow(GameUI.login)
	gUIManager:SetMainWindow(GameUI.login)

	App.networkManager:SendConnect()

end


function Game:InitMsgHandlers()
	LoginHandler.new():Register(gNetwork)
	SceneHandler.new():Register(gNetwork)
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





