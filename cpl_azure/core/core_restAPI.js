var rest;
(function (rest) {
    (function (enumRestStatus) {
        enumRestStatus[enumRestStatus["success"] = 0] = "success";
        enumRestStatus[enumRestStatus["failed"] = 1] = "failed";
    })(rest.enumRestStatus || (rest.enumRestStatus = {}));
    var enumRestStatus = rest.enumRestStatus;
    ;

    var RestReturn = (function () {
        function RestReturn(status, response, error) {
            if (error == null) {
                error = null;
            }
            ;
            this._status = status;
            this._response = response;
            this._error = error;
        }
        RestReturn.prototype.status = function () {
            return this._status;
        };
        RestReturn.prototype.response = function () {
            return this._response;
        };
        RestReturn.prototype.error = function () {
            return this._error;
        };
        return RestReturn;
    })();
    rest.RestReturn = RestReturn;

    var RESTRequest = (function () {
        function RESTRequest(url) {
            this._url = url;
        }
        RESTRequest.prototype.request = function (verb, additionalUrl, postData, callback) {
            var xhr = new XMLHttpRequest();
            try  {
                xhr.open(rest.eRequestVerb_Str(verb), this._url + additionalUrl, true);

                xhr.onload = function (evt) {
                    var xhrResponse = function () {
                        try  {
                            return JSON.parse(xhr.responseText);
                        } catch (ex) {
                            return xhr.responseText;
                        }
                    };

                    callback(new rest.RestReturn(rest.enumRestStatus.success, xhrResponse()));
                };

                xhr.onerror = function (err) {
                    callback(new rest.RestReturn(rest.enumRestStatus.failed, "", new Error(JSON.stringify(err))));
                };

                if (postData) {
                    xhr.send(JSON.stringify(postData));
                } else {
                    xhr.send();
                }
            } catch (ex) {
                throw ex;
            }
        };
        return RESTRequest;
    })();
    rest.RESTRequest = RESTRequest;

    (function (eRequestVerb) {
        eRequestVerb[eRequestVerb["GET"] = 0] = "GET";
        eRequestVerb[eRequestVerb["POST"] = 1] = "POST";
        eRequestVerb[eRequestVerb["PUT"] = 2] = "PUT";
        eRequestVerb[eRequestVerb["DELETE"] = 3] = "DELETE";
    })(rest.eRequestVerb || (rest.eRequestVerb = {}));
    var eRequestVerb = rest.eRequestVerb;
    ;

    function eRequestVerb_Str(verb) {
        var sVerb;
        if (verb === rest.eRequestVerb.GET) {
            sVerb = "GET";
        } else if (verb === rest.eRequestVerb.POST) {
            sVerb = "POST";
        } else if (verb === rest.eRequestVerb.PUT) {
            sVerb = "PUT";
        } else if (verb == rest.eRequestVerb.DELETE) {
            sVerb = "DELETE";
        }
        return sVerb;
    }
    rest.eRequestVerb_Str = eRequestVerb_Str;
})(rest || (rest = {}));
//# sourceMappingURL=core_restAPI.js.map
