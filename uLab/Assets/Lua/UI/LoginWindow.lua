
local gameObject
local transform

LoginWindow = {}
local this = LoginWindow

this.widgetTable = {}


function LoginWindow.OnInit(obj)
	gameObject = obj
	transform = obj.transform

	this.widgetTable.loginButton = Util.FindGameObject(gameObject, 'loginButton')
	UIEventListener.SetOnClick(this.widgetTable.loginButton, onLoginButtonClick)
	this.widgetTable.accountInput = Util.FindComponent(gameObject, 'InputField', "accountInputField")
	this.widgetTable.passwordInput = Util.FindComponent(gameObject, 'InputField', "passwordInputField")
	--
	this.widgetTable.accountInput.text = "locke001"
	this.widgetTable.passwordInput.text = "2333"
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
	local account = this.widgetTable.accountInput.text
	local password = this.widgetTable.passwordInput.text
	local canSend = true
	if string.len(account) > 10 or string.len(account) < 1 then
		print("Invalid Account!")
		canSend = false
	end
	if string.len(password) > 10 or string.len(password) < 1 then
		print("Invalid password!")
		canSend = false
	end
	if canSend then
		local login = {}
        login.account = account
        login.password = password
    
    local bytes = protobuf.encode("Lite.Protocol.cgLogin", login)
	App.networkManager:SendBytes(PBX.MsgID.cgLogin, bytes)
		--[[local msg = login_pb.cgLogin()
		msg.account = account
		msg.password = password
		Network.SendMessage(PBX.MsgID.cgLogin, msg)]]--
	end
end





