
local gameObject;
local transform;

MainWindow = {};
--local this = MainWindow;

function MainWindow.Awake(obj)
	gameObject = obj;
	transform = obj.transform;
	MainWindow.Init();
	LogInfo("Main Awake");
end

function MainWindow.Start(obj)

end

function MainWindow.Init()
	--this.btnOpen = transform:FindChild("open").gameObject;
end
