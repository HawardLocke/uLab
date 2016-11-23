
local gameObject
local transform

BagWindow = class("BagWindow",UIBase)
--local this = BagWindow

local buttonNames = { "bag", "role", "shop", "tip", "main"}


function BagWindow:OnInit(obj)
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

function BagWindow:OnEnter()
	
end

function BagWindow:OnExit()
	
end

function BagWindow:OnPause()
	
end

function BagWindow:OnResume()
	
end

function onBtnClick(go)
	local index = tonumber(string.sub(go.name, string.len(go.name)))
	
	if index == 1 then
		OpenWindow(GameUI.bag)
	elseif index == 2 then
		OpenWindow(GameUI.role)
	elseif index == 3 then
		OpenWindow(GameUI.shop)
	elseif index == 4 then
		OpenWindow(GameUI.tip)
	elseif index == 5 then
		BackToMainWindow()
	end
end

function onBackClick(go)
	CloseWindow(GameUI.bag)
end
