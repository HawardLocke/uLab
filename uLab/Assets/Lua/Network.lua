

Network = class('Network')


function Network:ctor()
	self.handlerMap = {}
end

function Network:OnStart()
	
end

function Network:OnDestroy()

end

function Network:SendMessage(msgId, data)
	App.networkManager:SendString(msgId, data:SerializeToString())
end

function Network:RegisterHandler(msgId, func)
	self.handlerMap[msgId] = func
end

-- msg handlers
function Network:OnConnect()
	print("connect success")
end

function Network:OnDisConnect()
	print("disconnected")
end

function Network:OnException()
	print("exception...")
end

function Network:OnMessage(msgId, data, len)
	--print('msg id ', msgId)
	local targetHandler = self.handlerMap[msgId]
	if not targetHandler then
		print("cannot find msg handler. msgId: ", msgId)
	else
		targetHandler(data, len)
	end
end

