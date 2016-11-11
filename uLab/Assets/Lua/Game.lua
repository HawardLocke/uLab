

require "Defines"
require "Functions"
require "Network"
require "UI/UIDefines"

require "protocol/MsgID"
require "protocol/login_pb"


Game = {};
local this = Game;



function Game.OnInitialize()
	OpenWindow(GameUI.bar);
	OpenWindow(GameUI.main);
	SetMainWindow(GameUI.main);

	App.networkManager:SendConnect();
	Network.RegisterMsgHandler(PBX.MsgID.Login, this.onLogin);

	--[[ test...
	local msg = login_pb.Login()
	msg.name = "locke007"
	msg.password = "2333"
  
	local pb_data = msg:SerializeToString()  -- Parse Example
	--[[print("str : "..pb_data);
	print("create:", msg.name, msg.password, pb_data)
	local msg1 = login_pb.Login()
	msg1:ParseFromString(pb_data)
	print("parser:", msg.name, msg.password, pb_data)]]--
	
	--App.networkManager:SendString(PBX.MsgID.Login, pb_data);
	--print("login id is ", PBX.MsgID.Login)
	--
end

function Game.onLogin(data)
	local msg = login_pb.Login();
	msg:ParseFromString(data);
	print("--recv loginret: "..msg.name..", "..msg.password);
end




