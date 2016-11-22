
local gameObject

DialogWindow = {}

function DialogWindow.OnInit(obj)
	gameObject = obj
	local ok = Util.FindGameObject(gameObject, 'buttons/button1')
	UIEventListener.SetOnClick(ok, onOkClick)
	local cancel = Util.FindGameObject(gameObject, 'buttons/back')
	UIEventListener.SetOnClick(cancel, onCancelClick)
end

function DialogWindow.OnEnter()
	
end

function DialogWindow.OnExit()
	
end

function DialogWindow.OnPause()
	
end

function DialogWindow.OnResume()
	
end

function onOkClick(go)
	CloseWindow(GameUI.dialog)
end

function onCancelClick(go)
	CloseWindow(GameUI.dialog)
end
