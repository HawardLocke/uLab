
local gameObject
local transform

local BagWindow = class("BagWindow",UIBase)
--local this = BagWindow

local buttonNames = { "bag", "role", "shop", "tip", "main"}


function BagWindow:OnInit(obj)
	gameObject = obj
	transform = obj.transform

	for i = 1, 5 do
		local btn = Util.FindGameObject(gameObject, 'buttons/button'..tostring(i))
		local label = Util.FindComponent(btn, 'Text', "Text")
		label.text = buttonNames[i]
		UIEventListener.SetOnClick(btn, self.onBtnClick, self)
	end

	local back = Util.FindGameObject(gameObject, "buttons/back")
	UIEventListener.SetOnClick(back, self.onBackClick, self)

end

function BagWindow:OnEnter()
	
end

function BagWindow:OnExit()
	
end

function BagWindow:OnPause()
	
end

function BagWindow:OnResume()
	
end

function BagWindow:onBtnClick(go)
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
		gUIManager:BackToMainWindow()
	end
end

function BagWindow:onBackClick(go)
	gUIManager:CloseWindow(GameUI.bag)
end

return BagWindow
