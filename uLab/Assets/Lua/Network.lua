

Network = {}
local this = Network;

Network.handlerMap = {};


function Network.Start()

end

function Network.Destroy()

end

function Network.RegisterMsgHandler(msgId, func)
	this.handlerMap[msgId] = func;
end

-- msg handlers
function Network.onConnect()
	print("connect success");
end

function Network.onDisConnect()
	print("disconnected");
end

function Network.onException()
	print("exception...");
end

function Network.onMessage(msgId, data)
	if not this.handlerMap[msgId] then
		print("no handler");
	else
		this.handlerMap[msgId](data);
	end
end
