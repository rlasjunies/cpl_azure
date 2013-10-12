/// <reference path="core_misc.ts" />
//TODO:
// 1) Create a method to trigger 1 callback when 1 one the message is arriving
//      the method could be called raced
//      usefull when there is a timeout expected. 1 message is related to a timeout
// 2) Create a method to trigger 1 callback when all the message expected have been arrived
//      this could be called meetingPoint
// 3) Record all the message in a buffer
//      this could be used to have a 'replay' function. We record all the messages arriving and then it's possible
//      to 'replay' them. It could be very usefull for testing purpose / load analysis
// 4) Compare message using diffObject
//      To replay we should plan to change few values (objects/msg properties) in the test scenario
//      The concept behind objectdiff could help supporting this use case
// 5) improve usability/readability
//      the current pub/sub is not very readable and (too?) verbose
//          core.app.PubSub.subscribe( new MsgTestStart(), function ( MsgTestStart ) {
//              paintsTests.test_postPaint();
//          } );
// 6) shoudl we create 2 message interfaces:
//      - 1 for command : an action triggered by an actor, the message should a "verb"
//      - and another one for events: an reaction following an actor command, the message should be an event
// 7) identicate if the message will be used only once. If yes, then it will automatically unsubscribe after use.
//      is it really usefull in really application (currently the remark is posted related to the test framework)
//
var core;
(function (core) {
    (function (pubsub) {
        var Thread = (function () {
            function Thread() {
                this.callbacks = [];
            }
            return Thread;
        })();
        pubsub.Thread = Thread;

        var CallBackSubscribbed = (function () {
            function CallBackSubscribbed(callback, args) {
                this.once = false;
                this.guid = core.misc.GUID_new();
                this.callback = callback;
                //    if ( args ) { this.args = args; }
            }
            return CallBackSubscribbed;
        })();
        pubsub.CallBackSubscribbed = CallBackSubscribbed;

        var PubSubToken = (function () {
            //constructor( public thread: string, public callback: ( msg: IPubSubMsg ) => void ) {}
            function PubSubToken(thread, guid) {
                this.thread = thread;
                this.guid = guid;
            }
            return PubSubToken;
        })();
        pubsub.PubSubToken = PubSubToken;

        var PubSub = (function () {
            function PubSub() {
                this._threads = [];
            }
            //        subscribe( msg: IPubSubMsg, callback: ( msg: IPubSubMsg, args?: any[] ) => void , args?: any[]  ): PubSubToken {
            /**
            * Regsiter a callback function to a IPubSubMsg. When the message will be published the function will be called by the framework
            *
            * @Param msg a IPubSubMsg class .... e.i.: new cmdLoadPagePaints()
            * @Param the function to callback ... e.i: function ( evt: evtTestFinished )
            * @Param PubSubToken ... unique number of the registration in order to unresgister
            */
            PubSub.prototype.subscribe = function (msg, callback) {
                //is a new thread?
                var thread = core.misc.getObjectClass(msg);
                if (!this._threads[thread]) {
                    this._threads[thread] = new Thread();
                }

                //Add the callback to the thread
                var t = this._threads[thread];

                //var cb = new CallBackSubscribbed( callback,args);
                var cb = new CallBackSubscribbed(callback);

                t.callbacks.push(cb);

                //this._threads[thread].subscribed.push( callback );
                return new PubSubToken(thread, cb.guid);
            };

            //subscribeOnce( msg: IPubSubMsg, callback: ( msg: IPubSubMsg, args?: any[] ) => void , args?: any[] ): void {
            PubSub.prototype.subscribeOnce = function (msg, callback) {
                //var token = this.subscribe( msg, callback, args );
                var token = this.subscribe(msg, callback);

                if (this._threads[token.thread]) {
                    var thread = this._threads[token.thread];
                    var len = thread.callbacks.length;

                    while (len--) {
                        if (thread.callbacks[len].guid === token.guid) {
                            thread.callbacks[len].once = true;
                        }
                    }
                }
            };

            PubSub.prototype.unsubscribe = function (token) {
                if (this._threads[token.thread]) {
                    var thread = this._threads[token.thread];
                    var len = thread.callbacks.length;

                    while (len--) {
                        if (thread.callbacks[len].guid === token.guid) {
                            thread.callbacks.splice(len, 1);
                        }
                    }
                }
            };

            PubSub.prototype.publish = function (msg) {
                var sThread = core.misc.getObjectClass(msg);
                if (this._threads[sThread]) {
                    var oThread = this._threads[sThread];
                    var len = oThread.callbacks.length;

                    while (len--) {
                        if (oThread.callbacks[len].args) {
                            oThread.callbacks[len].callback(msg, oThread.callbacks[len].args);
                        } else {
                            oThread.callbacks[len].callback(msg);
                        }
                        if (oThread.callbacks[len].once) {
                            core.Logger.log("PubSub.RemoveOnceMessages");
                            oThread.callbacks.splice(len, 1);
                        }
                    }
                }
            };
            return PubSub;
        })();
        pubsub.PubSub = PubSub;
    })(core.pubsub || (core.pubsub = {}));
    var pubsub = core.pubsub;
})(core || (core = {}));
