
local gameObject
local transform

UIBase =  class("UIBase", function ()
	
end)

function UIBase:OnInit(obj)

end

function UIBase:OnEnter()
	
end

function UIBase:OnExit()
	
end

function UIBase:OnPause()
	
end

function UIBase:OnResume()
	
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
