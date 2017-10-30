
local gameObject

local TipWindow = class("TipWindow",UIBase)

function TipWindow:OnInit(obj)
	gameObject = obj
	local ok = Util.FindGameObject(gameObject, 'buttons/button1')
	UIEventListener.SetOnClick(ok, onOkClick)
	local cancel = Util.FindGameObject(gameObject, 'buttons/back')
	UIEventListener.SetOnClick(cancel, onCancelClick)
end

function TipWindow:OnEnter()
	
end

function TipWindow:OnExit()
	
end

function TipWindow:OnPause()
	
end

function TipWindow:OnResume()
	
end

function onOkClick(go)
	CloseWindow(GameUI.tip)
end

function onCancelClick(go)
	CloseWindow(GameUI.tip)
end

return ShopWindow