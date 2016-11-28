
local gameObject
local transform

BarWindow = class("BagWindow",UIBase)
--local this = BarWindow

function BarWindow.OnInit(obj)
	gameObject = obj
	transform = obj.transform
	--LogInfo("Bar OnInit")
end

function BarWindow:OnEnter()
	--LogInfo("Bar OnEnter")
end

function BarWindow:OnExit()
	--LogInfo("Bar OnExit")
end

function BarWindow:OnPause()
	--LogInfo("Bar OnPause")
end

function BarWindow:OnResume()
	--LogInfo("Bar OnResume")
end
