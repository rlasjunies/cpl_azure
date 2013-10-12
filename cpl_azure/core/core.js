/// <reference path="core_restAPI.ts" />
/// <reference path="core_pubsub.ts" />
var gApp;
var core;
(function (core) {
    var App = (function () {
        //constructor(w: Window, debug?: Boolean ) {
        function App(site) {
            //if (debug == null) { debug = false };
            //this._debug = debug;
            this.PubSub = new core.pubsub.PubSub();
            this.site = site;
        }
        return App;
    })();
    core.App = App;

    var Logger = (function () {
        function Logger() {
        }
        Logger.log = function (message) {
            if (typeof window.console !== 'undefined') {
                window.console.log(message);
            }
        };
        return Logger;
    })();
    core.Logger = Logger;
})(core || (core = {}));
