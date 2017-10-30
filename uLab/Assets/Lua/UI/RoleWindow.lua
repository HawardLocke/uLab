
local gameObject
local transform

local RoleWindow = class("RoleWindow",UIBase)
--local this = RoleWindow

local buttonNames = { "bag", "role", "shop", "tip", "dialog"}


function RoleWindow:OnInit(obj)
	gameObject = obj
	transform = obj.transform

	for i = 1, 5 do
		local btn = Util.FindGameObject(gameObject, 'buttons/button'..tostring(i))
		local label = Util.FindComponent(btn, 'Text', "Text")
		label.text = buttonNames[i]
		UIEventListener.SetOnClick(btn, onBtnClick)
	end

	local back = Util.FindGameObject(gameObject, "buttons/back")
	UIEventListener.SetOnClick(back, onBackClick)

end

function RoleWindow:OnEnter()
	
end

function RoleWindow:OnExit()
	
end

function RoleWindow:OnPause()
	
end

function RoleWindow:OnResume()
	
end

function onBtnClick(go)
	local index = tonumber(string.sub(go.name, string.len(go.name)))
	
	if index == 1 then
		gUIManager:OpenWindow(GameUI.bag)
	elseif index == 2 then
		gUIManager:OpenWindow(GameUI.role)
	elseif index == 3 then
		gUIManager:OpenWindow(GameUI.shop)
	elseif index == 4 then
		gUIManager:OpenWindow(GameUI.tip)
	elseif index == 5 then
		gUIManager:OpenWindow(GameUI.dialog)
	end
end

function onBackClick(go)
	CloseWindow(GameUI.role)
end

return RoleWindow
