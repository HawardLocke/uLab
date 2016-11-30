
UIManager = class('UIManager')

function UIManager:ctor()
	self.windowTable = {}
end

function UIManager:OpenWindow(name)
	if not self.windowTable[name] then
		self.windowTable[name] = require('UI/'..name).new()
	end
	Lite.App.uiManager:OpenWindow(name)
end

function UIManager:CloseWindow(name)
	self.windowTable[name] = nil
	Lite.App.uiManager:CloseWindow(name)
end

function UIManager:SetMainWindow(name)
	Lite.App.uiManager:SetMainWindow(name)
end

function UIManager:BackToMainWindow()
	Lite.App.uiManager:BackToMainWindow()
end




