

require "Defines"
require "Functions"
require "Network"
require "UI/UIDefines"

require "protocol/MsgID"
require "protocol/login_pb"


Game = {};
local this = Game;



function Game.OnInitialize()
	OpenWindow(GameUI.login);
	SetMainWindow(GameUI.login);

	App.networkManager:SendConnect();
	Network.RegisterHandler(PBX.MsgID.LoginResponse, this.onLoginResult);
	Network.RegisterHandler(PBX.MsgID.EnterGameResponse, this.onEnterGameResult);

	--[[test...
	local msg = login_pb.Login()
	msg.name = "locke007"
	msg.password = "2333"
  
	local pb_data = msg:SerializeToString()  -- Parse Example
	print("str : "..pb_data);
	print("create:", msg.name, msg.password, pb_data)
	local msg1 = login_pb.Login()
	msg1:ParseFromString(pb_data)
	print("parser:", msg.name, msg.password, pb_data)
	
	App.networkManager:SendString(PBX.MsgID.Login, pb_data);
	print("login id is ", PBX.MsgID.Login)
	]]--
end

function Game.onLoginResult(data)
	local msg = login_pb.LoginResponse();
	msg:ParseFromString(data);
	print("--recv login result: ", msg.result);
	if  msg.result > 0 then
		print("login failed. error code: ", msg.result);
	else
		local enterGameMsg = login_pb.EnterGameRequest();
		enterGameMsg.roleIndex = 1;
		Network.SendMessage(PBX.MsgID.EnterGameRequest, enterGameMsg);
	end
end

function Game.onEnterGameResult(data)
	local recvMsg = login_pb.EnterGameResponse();
	recvMsg:ParseFromString(data);
	print("--recv enter game result: ", recvMsg.result);
	if recvMsg.result > 0 then
		print("login failed. error code: ", msg.result);
	else
		--enter scene
		Util.LoadScene("LevelScene");
	end
end








