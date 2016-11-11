
local gameObject;
local transform;

MainWindow = {};
local this = MainWindow;

local buttonNames = { "bag", "role", "shop", "tip", "dialog", "show main", "send chat", "??", "reset" };


function MainWindow.OnInit(obj)
	gameObject = obj;
	transform = obj.transform;
	--LogInfo("Main OnInit");
	for i = 1, 9 do
		local btn = Util.FindGameObject(gameObject, 'buttons/button'..tostring(i));
		local label = Util.FindComponent(btn, 'Text', "Text");
		label.text = buttonNames[i];
		UIEventListener.SetOnClick(btn, onBtnClick);
	end
end

function MainWindow.OnEnter()
	
end

function MainWindow.OnExit()
	
end

function MainWindow.OnPause()
	
end

function MainWindow.OnResume()
	
end

function onBtnClick(go)
	local index = tonumber(string.sub(go.name, string.len(go.name)));
	
	if index == 1 then
		OpenWindow(GameUI.bag);
	elseif index == 2 then
		OpenWindow(GameUI.role);
	elseif index == 3 then
		OpenWindow(GameUI.shop);
	elseif index == 4 then
		OpenWindow(GameUI.tip);
	elseif index == 5 then
		OpenWindow(GameUI.dialog);	
	elseif index == 6 then
		BackToMainWindow();
	elseif index == 7 then
		this.SendChat();
	elseif index == 8 then
		--
	elseif index == 9 then
		App.uiManager:Cleanup();
	end
end

function MainWindow.SendChat()
	local msg = login_pb.Login();
	msg.name = "locke007";
	msg.password = "2333";
	App.networkManager:SendString(PBX.MsgID.Login, msg:SerializeToString());
end