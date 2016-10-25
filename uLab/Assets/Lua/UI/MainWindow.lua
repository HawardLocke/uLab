
local gameObject;
local transform;

MainWindow = {};
--local this = MainWindow;

function MainWindow.Awake(obj)
	gameObject = obj;
	transform = obj.transform;
	MainWindow.Init();
end

function MainWindow.Init()
	--this.btnOpen = transform:FindChild("open").gameObject;
	Locke.Log.Error("MainWindow~");
end
