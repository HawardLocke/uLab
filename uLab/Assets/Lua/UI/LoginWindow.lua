

LoginWindow = class("LoginWindow")


function LoginWindow:OnInit(obj)
	self.gameObject = obj
	--self.transform = obj.transform

	self.widgetTable = {}
	self.widgetTable.loginButton = Util.FindGameObject(self.gameObject, 'loginButton')
	UIEventListener.SetOnClick(self.widgetTable.loginButton, self.onLoginButtonClick)
	self.widgetTable.accountInput = Util.FindComponent(self.gameObject, 'InputField', "accountInputField")
	self.widgetTable.passwordInput = Util.FindComponent(self.gameObject, 'InputField', "passwordInputField")
	--
	self.widgetTable.accountInput.text = "locke001"
	self.widgetTable.passwordInput.text = "2333"
end

function LoginWindow:OnEnter()
	
end

function LoginWindow:OnExit()
	
end

function LoginWindow:OnPause()
	
end

function LoginWindow:OnResume()
	
end

function LoginWindow:onLoginButtonClick(go)
	local account = self.widgetTable.accountInput.text
	local password = self.widgetTable.passwordInput.text
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


return LoginWindow


