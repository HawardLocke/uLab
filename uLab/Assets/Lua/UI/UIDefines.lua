
-- defines of ui, and window infos.


local UIShowMode = 
{
	Normal = 0,		-- for example : bag, equip, rank. NOTE: only normal window can be tracing back.
	Main = 1,		-- Main Window. cannot be closed.
	Fixed = 2,		-- cannot be closed.
	Popup = 3,		-- for example : messagebox, floating window.
};

local UIOpenAction =
{
	DoNothing = 0,
	HideNormalsMains = 1,
	HideAll = 2,
};

local UIBackgroundMode =
{
	None = 0,			-- no bg, no raycast.
	Transparent = 1,	-- transparent bg, with raycast.
	Dark = 2,			-- dark bag, with raycast.
};


GameUI =
{
	main = "MainWindow",
	bar = "BarWindow",
	role = "RoleWindow",
	bag = "BagWindow",
	shop = "ShopWindow",
	tip = "TipWindow",
	dialog = "DialogWindow",
};

RegisterWindow("ui/MainWindow",
	UIShowMode.Main,
	UIOpenAction.DoNothing,
	UIBackgroundMode.None
	);

RegisterWindow("ui/BarWindow",
	UIShowMode.Fixed,
	UIOpenAction.DoNothing,
	UIBackgroundMode.None
	);

RegisterWindow("ui/RoleWindow",
	UIShowMode.Normal,
	UIOpenAction.HideNormalsMains,
	UIBackgroundMode.Transparent
	);

RegisterWindow("ui/ShopWindow",
	UIShowMode.Normal,
	UIOpenAction.HideNormalsMains,
	UIBackgroundMode.Transparent
	);

RegisterWindow("ui/BagWindow",
	UIShowMode.Normal,
	UIOpenAction.HideNormalsMains,
	UIBackgroundMode.Transparent
	);

RegisterWindow("ui/TipWindow",
	UIShowMode.Popup,
	UIOpenAction.DoNothing,
	UIBackgroundMode.Dark
	);

RegisterWindow("ui/DialogWindow",
	UIShowMode.Normal,
	UIOpenAction.HideAll,
	UIBackgroundMode.Dark
	);
