
local gameObject;
local transform;

LoginWindow = {};
local this = LoginWindow;

local widgetTable = {};


function LoginWindow.OnInit(obj)
	gameObject = obj;
	transform = obj.transform;

	widgetTable.loginButton = Util.FindGameObject(gameObject, 'loginButton');
	UIEventListener.SetOnClick(widgetTable.loginButton, onLoginButtonClick);
	widgetTable.accountInput = Util.FindComponent(gameObject, 'InputField', "accountInputField");
	widgetTable.passwordInput = Util.FindComponent(gameObject, 'InputField', "passwordInputField");
	
end

function LoginWindow.OnEnter()
	
end

function LoginWindow.OnExit()
	
end

function LoginWindow.OnPause()
	
end

function LoginWindow.OnResume()
	
end

function onLoginButtonClick(go)
	local account = widgetTable.accountInput.text;
	local password = widgetTable.passwordInput.text;
	local canSend = true;
	if string.len(account) > 10 or string.len(account) < 1 then
		print("Invalid Account!");
		canSend = false;
	end
	if string.len(password) > 10 or string.len(password) < 1 then
		print("Invalid password!");
		canSend = false;
	end
	if canSend then
		local msg = login_pb.LoginRequest();
		msg.account = account;
		msg.password = password;
		Network.SendMessage(PBX.MsgID.LoginRequest, msg);
	end
end





