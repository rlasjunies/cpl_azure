/// <reference path="../core/core.ts" />
/// <reference path="../core/core_restAPI.ts" />
/// <reference path="../core/core_pubsub.ts" />
/// <reference path="../libs/typings/jquery/jquery.d.ts"/>
module models {
    export module aboutMes {
        // PubSub Messages
        export class evtBiographyGetted implements core.pubsub.IPubSubMsg, core.pubsub.IPubSubEvt_FunctionReturn {
            constructor(public status: core.misc.enumEntityStatus, public value: any, public error: Error) { }
        }

        //Entities stream
        export class AboutMes {
            private _root: string = "api/AboutMeAPI";

            get() {

                var JQryAjxSetting: JQueryAjaxSettings = {
                    url: this._root,
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    error: function (xhr, status, error) {
                        gApp.PubSub.publish(new evtBiographyGetted(core.misc.enumEntityStatus.failed, null, new Error(error.toString())));
                    },
                    success: function (data) {
                        gApp.PubSub.publish(new evtBiographyGetted(core.misc.enumEntityStatus.success, data, null));
                    },
                    jsonpCallback: 'itDoesntMatterNotAFunction',
                };
                $.ajax(JQryAjxSetting);
            }
        }

        ////entity data
        //export class AboutMe implements core.misc.IEntities {
        //    constructor(public id: string, public Biography: string) {
        //    }
        //}
    }
}