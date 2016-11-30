
--port to cs

CSharpPort = {}

function CSharpPort.Network_OnStart()
	gNetwork:OnStart()
end

function CSharpPort.Network_OnDestroy()
	gNetwork:OnDestroy()
end

function CSharpPort.Network_OnMessage()
	gNetwork:OnMessage()
end

function CSharpPort.Game_OnInitialize()
	gGame:OnInitialize()
end

function CSharpPort.Window_OnInit(name,obj)
	if gUIManager.windowTable[name] then
		gUIManager.windowTable[name]:OnInit(obj)
	end
end
function CSharpPort.Window_OnEnter(name)
	if gUIManager.windowTable[name] then
		gUIManager.windowTable[name]:OnEnter()
	end
end
function CSharpPort.Window_OnExit(name)
	if gUIManager.windowTable[name] then
		gUIManager.windowTable[name]:OnExit()
	end
end
function CSharpPort.Window_OnResume(name)
	if gUIManager.windowTable[name] then
		gUIManager.windowTable[name]:OnResume()
	end
end
function CSharpPort.Window_OnPause(name)
	if gUIManager.windowTable[name] then
		gUIManager.windowTable[name]:OnPause()
	end
end

