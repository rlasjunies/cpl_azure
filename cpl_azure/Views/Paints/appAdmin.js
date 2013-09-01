/// <reference path="../../ClientFramework/core_pubsub.ts" />
/// <reference path="../../ClientFramework/core.ts" />
/// <reference path="../../ClientFramework/core_restAPI.ts" />
/// <reference path="../../Scripts/typings/jquerymobile/jquerymobile.d.ts" />
/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../models/paints.ts" />
var cmdLoadPaints = (function () {
    function cmdLoadPaints() {
    }
    return cmdLoadPaints;
})();
;
var evtPaintsLoaded = (function () {
    function evtPaintsLoaded() {
    }
    return evtPaintsLoaded;
})();
;

var cmdLoadPaint = (function () {
    function cmdLoadPaint(paintId) {
        this.paintId = paintId;
    }
    return cmdLoadPaint;
})();
;
var evtPaintLoaded = (function () {
    function evtPaintLoaded(paint) {
        this.paint = paint;
    }
    return evtPaintLoaded;
})();
;

var cmdUpdatePaint = (function () {
    function cmdUpdatePaint(paintId) {
        this.paintId = paintId;
    }
    return cmdUpdatePaint;
})();
;
var evtPaintUpdated = (function () {
    function evtPaintUpdated(paint) {
        this.paint = paint;
    }
    return evtPaintUpdated;
})();
;

//class cmdJumpToPage_Paints
//class evtPageShowned-Paints
//class cmdJumpToPage_Paint
//class evtPageShowned_Paint
//class cmdJumpToPage_Pictures
//class evtPageShowned_Pictures
//class cmdQuit
//class cmdRequestLogin
//class evtUserLogged
window.onload = function () {
    var site = new rest.RESTRequest("http://cpairelasjunies.com/rest.php/");
    gApp = new core.App(site);
    gSite = new rest.RESTRequest("http://cpairelasjunies.com/rest.php/");

    gApp.PubSub.subscribe(new cmdLoadPaints(), function (cmdLoadPaint) {
        var ps;
        ps = new models.paints.Paints();
        ps.getAll();
    });
    gApp.PubSub.subscribe(new models.paints.evtPaintsGetted(null, null, null), function (evt) {
        if (evt.error) {
            alert("error: loading the paintings data!!");
        } else {
        }
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
    });
};
