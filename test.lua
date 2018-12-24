box.cfg{
}

http_server = require('http.server')
fiber = require('fiber')

--Функции для обработки через http server
function http_handler_acceptNewMatch(self)
        local matchid = tonumber(self:param('matchid'))
        local result = true
        local error = nil
        local resp = self:render({json={result=result,error=error}})
        return resp
end



server = http_server.new('0.0.0.0', 8777)
server:route({ path = '/test'},http_handler_acceptNewMatch)
server:start()
