

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

	local msg = login_pb.Login()
	msg.name = "foo"
	msg.password = "bar"
  
	local pb_data = msg:SerializeToString()  -- Parse Example

	--print("create:", msg.name, msg.password, pb_data)

	local msg1 = login_pb.Login()
	msg1:ParseFromString(pb_data)
	--print("parser:", msg.name, msg.password, pb_data)
	
	--print("login id is ", PBX.MsgID.Login)

	App.networkManager:SendConnect();

	Network.RegisterMsgHandler(PBX.MsgID.Login, this.onLogin);

end

function onLogin(data)
	local msg = login_pb.Login();
	msg:ParseFromString(data);
	print(msg.name +", " + msg.password);
end




