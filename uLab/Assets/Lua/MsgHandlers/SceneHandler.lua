

SceneHandler = {};
local this = SceneHandler;


function SceneHandler.Register()
	Network.RegisterHandler(PBX.MsgID.gcEnterScene, this.OnEnterScene);
	Network.RegisterHandler(PBX.MsgID.gcExitSceneRet, this.OnExitSceneRet);
	Network.RegisterHandler(PBX.MsgID.gcOtherEnterScene, this.OnOtherEnterScene);
	Network.RegisterHandler(PBX.MsgID.gcNearbyPlayerInfo, this.OnNearbyPlayerInfo);
	Network.RegisterHandler(PBX.MsgID.gcMoveTo, this.OnMoveTo);
	print("SceneHandler.Register.");
end


--handlers

function SceneHandler.OnEnterScene(data)
	local msg = scene_pb.gcEnterScene();
	msg:ParseFromString(data);
	print("recv enter scene.");
	Util.LoadScene("LevelScene");
end

function SceneHandler.OnExitSceneRet(data)
	local recvMsg = scene_pb.gcExitSceneRet();
	recvMsg:ParseFromString(data);
	print("recv exit scene.");
end

function SceneHandler.OnOtherEnterScene(data)
	local msg = scene_pb.gcOtherEnterScene();
	msg:ParseFromString(data);
	print("recv other enter scene.");
	App.entityManager:AddPlayer(msg.playerInfo.playerGuid, msg.playerInfo.x, msg.playerInfo.y);
end

function SceneHandler.OnNearbyPlayerInfo(data)
	local msg = scene_pb.gcNearbyPlayerInfo();
	msg:ParseFromString(data);
	print("recv nearby player info.");
	for playerInfo in msg.playerInfoList do
		App.entityManager:AddPlayer(playerInfo.playerGuid, playerInfo.x, playerInfo.y);
	end
end

function SceneHandler.OnMoveTo(data)
	local msg = scene_pb.gcMoveTo();
	msg:ParseFromString(data);
	print("recv move to.");
end

