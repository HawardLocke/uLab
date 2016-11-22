

LoginHandler = {}
local this = LoginHandler


function LoginHandler.Register()
	Network.RegisterHandler(PBX.MsgID.gcLoginRet, this.OnLoginRet)
	Network.RegisterHandler(PBX.MsgID.gcLogoutRet, this.OnLogoutRet)
	Network.RegisterHandler(PBX.MsgID.gcEnterGameRet, this.OnEnterGameRet)
end


--handlers

function LoginHandler.OnLoginRet(data, len)
	local msg = protobuf.decode("Lite.Protocol.gcLoginRet", data, len)
	--local msg = login_pb.gcLoginRet()
	--msg:ParseFromString(data)
	print("--recv login result: ", msg.result)
	if  msg.result > 0 then
		print("login failed. error code: ", msg.result)
	else
		--App.networkManager:SendBytes(PBX.MsgID.cgLogin, bytes)
		local enterGameMsg = {}
		enterGameMsg.roleIndex = 1
		--Network.SendMessage(PBX.MsgID.cgEnterGame, enterGameMsg)
		local bytes = protobuf.encode("Lite.Protocol.cgEnterGame", enterGameMsg)
		App.networkManager:SendBytes(PBX.MsgID.cgEnterGame, bytes)
	end
end

function LoginHandler.OnLogoutRet(data, len)
	local msg = login_pb.gcLogoutRet()
	msg:ParseFromString(data)
	print("--recv logout result: ", msg.result)
	if  msg.result > 0 then
		print("logout failed. error code: ", msg.result)
	else
		print("logout success.")
	end
end

function LoginHandler.OnEnterGameRet(data, len)
	local recvMsg = protobuf.decode("Lite.Protocol.gcEnterGameRet", data, len)
	--local recvMsg = login_pb.gcEnterGameRet()
	--recvMsg:ParseFromString(data)
	print("--recv enter game result: ", recvMsg.result)
	if recvMsg.result > 0 then
		print("login failed. error code: ", msg.result)
	else
		print("enter game success.")
	end
end

