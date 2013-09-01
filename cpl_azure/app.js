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

var cmdJumpToPage_Paints = (function () {
    function cmdJumpToPage_Paints() {
    }
    return cmdJumpToPage_Paints;
})();
;

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
            var li;
            evt.value.forEach(function (key, val) {
                li = $('#listPaints').append("<li id='" + key.Name + "'><h3>" + key.Name + "</h3></li>");
                li.on('click', function () {
                    alert('item ' + key.Name + ' pressed');
                });
            });
            $('listPaints').listview("refresh");
        }
    });

    gApp.PubSub.subscribe(new cmdJumpToPage_Paints(), function (cmdJumpToPage_Paints) {
        $.mobile.changePage("index.html#pagePaints", {
            transition: "pop"
        });
    });

    gApp.PubSub.subscribe(new cmdJumpToPage_AboutMe(), function (cmdJumpToPage_AboutMe) {
        $.mobile.changePage("index.html#pageMe", {
            transition: "pop"
        });
    });

    gApp.PubSub.subscribe(new cmdLoadBiography(), function (cmdLoadBiography) {
        var bio;
        bio = new models.aboutMes.AboutMes();
        bio.get();
    });

    gApp.PubSub.subscribe(new models.aboutMes.evtBiographyGetted(null, null, null), function (evt) {
        $('#biography').html(evt.value.Biographie);
    });

    gApp.PubSub.publish(new cmdLoadPaints());
    gApp.PubSub.publish(new cmdLoadBiography());
};
//# sourceMappingURL=app.js.map
