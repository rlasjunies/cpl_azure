//TODO compare with the usage of request.JSON (jQuery)
//new Request.JSON(
//{
//    url: '/list.json',
//    onSuccess: function ( json ) {
//        json.list.each( function ( key, val ) {
//            new Element( 'LI' )
//                .set( 'text', val )
//                .addEvent( 'click', function () {
//                    alert( 'item ' + key + ' pressed' );
//                    // alert('item '+val.id+' pressed');
//                    // considering val is an object instead of raw string, this way you must change set to something like this set('text', val.text)
//                } )
//                .inject( $( 'list' ) );
//            // any other thing you want add to your list item
//        } );
//    }
//} ).get();
//<ul id = "list" > < / ul >
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
        // Note that you can also
        // use the new XMLHttpRequest Level 2
        // events
        // See here
        //// http://www.w3.org/TR/XMLHttpRequest/
        //var xhr = new XMLHttpRequest();
        //xhr.onreadystatechange = function () {
        //    if (xhr.readyState == 4) {
        //        if (xhr.status == 200) {
        //            // All right - data is stored in xhr.responseText
        //        }
        //        else {
        //            // Server responded with another status code!
        //        }
        //    }
        //}
        //xhr.open("GET", "yourscript.php");
        //xhr.send();
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
                    //if ( typeof ( postData ) === "string" ) {
                    xhr.send(JSON.stringify(postData));
                    //} else {
                    //    xhr.send( postData );
                    //}
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
