/// <reference path="core/core_pubsub.ts" />
/// <reference path="core/core.ts" />
/// <reference path="core/core_restAPI.ts" />
/// <reference path="libs/typings/jquerymobile/jquerymobile.d.ts" />
/// <reference path="libs/typings/jquery/jquery.d.ts" />
/// <reference path="T4TS.d.ts"/>
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

var cmdLoadBiography = (function () {
    function cmdLoadBiography() {
    }
    return cmdLoadBiography;
})();
;

//class evtBiographyLoaded implements core.pubsub.IPubSubMsg { };
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

var cmdJumpToPage_AboutMe = (function () {
    function cmdJumpToPage_AboutMe() {
    }
    return cmdJumpToPage_AboutMe;
})();
;

//class evtPageShowned_AboutMe implements core.pubsub.IPubSubMsg { };
var cmdJumpToPage_Paints = (function () {
    function cmdJumpToPage_Paints() {
    }
    return cmdJumpToPage_Paints;
})();
;

//class evtPageShowned_Paints implements core.pubsub.IPubSubMsg { };
//class cmdJumpToPage_Paint
//class evtPageShowned_Paint
//class cmdJumpToPage_Pictures
//class evtPageShowned_Pictures
//class cmdQuit
//class cmdRequestLogin
//class evtUserLogged
window.onload = function () {
    var site = new rest.RESTRequest("http://localhost:50652/");
    gApp = new core.App(site);

    gApp.PubSub.subscribe(new cmdLoadPaints(), function (cmdLoadPaint) {
        var ps;
        ps = new models.paints.Paints();
        ps.getAll();
    });

    gApp.PubSub.subscribe(new models.paints.evtPaintsGetted(null, null, null), function (evt) {
        if (evt.error) {
            alert("error: loading the paintings data!!:" + evt.error);
        } else {
            //alert( "paintings data Loaded!!:" + evt.value );
            //evt.value.forEach(
            //    function ( key, val ) {
            //        var li: JQuery;
            //        li = $( '#listPaints' ).append( "<li><a href='#' class='painting'><img src='" + key.Picture + "'/></a></li>" );
            //        li.click(
            //            function () {
            //                alert( 'item ' + key.Name + ' pressed' );
            //            }
            //        );
            //        //li.on( 'click',
            //        //    function () {
            //        //        alert( 'item ' + key.Name + ' pressed' );
            //        //    }
            //        //);
            //    }
            //    );
            var items = [];

            jQuery.each(evt.value, function (key, val) {
                items.push("<li><a href='#' class='painting'><img src='" + val.Picture + "'/> <h2>" + val.Name + "</h2><p>" + val.Description + "</p></li>");
            });

            var list = $("#listPaints");
            list.append(items);
            list.bind('pageinit', function () {
                list.listview('refresh');
            });
        }
        $.mobile.loading('hide');
    });

    gApp.PubSub.subscribe(new cmdJumpToPage_Paints(), function (cmdJumpToPage_Paints) {
        $.mobile.changePage("index.html#pagePaints");
    });

    gApp.PubSub.subscribe(new cmdJumpToPage_AboutMe(), function (cmdJumpToPage_AboutMe) {
        $.mobile.changePage("index.html#pageMe");
    });

    gApp.PubSub.subscribe(new cmdLoadBiography(), function (cmdLoadBiography) {
        var bio;
        bio = new models.aboutMes.AboutMes();
        $.mobile.loading('show');
        bio.get();
    });

    gApp.PubSub.subscribe(new models.aboutMes.evtBiographyGetted(null, null, null), function (evt) {
        $('#biography').html(evt.value.Biographie);
        //$('#biography').listview("refresh");
    });

    gApp.PubSub.publish(new cmdLoadPaints());
    gApp.PubSub.publish(new cmdLoadBiography());
};
