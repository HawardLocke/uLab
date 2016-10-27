
local gameObject;
local transform;

BarWindow = {};
--local this = BarWindow;

function BarWindow.Awake(obj)
	gameObject = obj;
	transform = obj.transform;
	BarWindow.Init();
	LogInfo("Bar Awake");
end

function BarWindow.Start(obj)
	-- body
end

function BarWindow.Init()
	--this.btnOpen = transform:FindChild("open").gameObject;
end
