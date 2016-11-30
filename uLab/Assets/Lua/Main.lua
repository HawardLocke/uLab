
require 'CSharpPort'
require 'Game'
require 'Network'


gGame = Game.new()
gNetwork = Network.new()
gUIManager = UIManager.new()


--主入口函数。从这里开始lua逻辑
function Main()					
	
end

--场景切换通知
function OnLevelWasLoaded(level)
	Time.timeSinceLevelLoad = 0
end

