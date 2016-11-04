

require "Defines"
require "Functions"
require "UI/UIDefines"

require "protocol/proto_login_pb"


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

	local msg = proto_login_pb.Login()
msg.name = "foo"
msg.password = "bar"
  
local pb_data = msg:SerializeToString()  -- Parse Example

print("create:", msg.name, msg.password, pb_data)

local msg1 = proto_login_pb.Login()
msg1:ParseFromString(pb_data)
print("parser:", msg.name, msg.password, pb_data)

end




