

Network = {}
local this = Network

Network.handlerMap = {}


function Network.Start()

end

function Network.Destroy()

end

function Network.SendMessage(msgId, data)
	App.networkManager:SendString(msgId, data:SerializeToString())
end

function Network.RegisterHandler(msgId, func)
	this.handlerMap[msgId] = func
end

-- msg handlers
function Network.onConnect()
	print("connect success")
end

function Network.onDisConnect()
	print("disconnected")
end

function Network.onException()
	print("exception...")
end

function Network.onMessage(msgId, data, len)
	--print('msg id ', msgId)
	local targetHandler = this.handlerMap[msgId]
	if not targetHandler then
		print("cannot find msg handler. msgId: ", msgId)
	else
		targetHandler(data, len)
	end
end
