/// <reference path="core/core_pubsub.ts" />
/// <reference path="core/core.ts" />
/// <reference path="core/core_restAPI.ts" />
/// <reference path="libs/typings/jquerymobile/jquerymobile.d.ts" />
/// <reference path="libs/typings/jquery/jquery.d.ts" />
/// <reference path="models/paints.ts" />

class cmdLoadPaints implements core.pubsub.IPubSubMsg { };
class evtPaintsLoaded implements core.pubsub.IPubSubMsg { };

class cmdLoadBiography implements core.pubsub.IPubSubMsg { };
//class evtBiographyLoaded implements core.pubsub.IPubSubMsg { };

class cmdLoadPaint implements core.pubsub.IPubSubMsg { constructor(public paintId: string) { }  };
class evtPaintLoaded implements core.pubsub.IPubSubMsg { constructor(public paint: models.paints.Paint) { } };

class cmdUpdatePaint implements core.pubsub.IPubSubMsg { constructor(public paintId: string) { }};
class evtPaintUpdated implements core.pubsub.IPubSubMsg { constructor(public paint: models.paints.Paint) { } };

class cmdJumpToPage_AboutMe implements core.pubsub.IPubSubMsg { };
//class evtPageShowned_AboutMe implements core.pubsub.IPubSubMsg { };

class cmdJumpToPage_Paints implements core.pubsub.IPubSubMsg { };
//class evtPageShowned_Paints implements core.pubsub.IPubSubMsg { };

//class cmdJumpToPage_Paint
//class evtPageShowned_Paint
//class cmdJumpToPage_Pictures
//class evtPageShowned_Pictures   

//class cmdQuit
//class cmdRequestLogin
//class evtUserLogged

window.onload = () => {
    var site = new rest.RESTRequest("http://localhost:50652/");
    gApp = new core.App(site);

    gApp.PubSub.subscribe(new cmdLoadPaints, function (cmdLoadPaint) {
        var ps: models.paints.Paints;
        ps = new models.paints.Paints();
        ps.getAll();
    });

    gApp.PubSub.subscribe( new models.paints.evtPaintsGetted( null, null, null ), function ( evt: models.paints.evtPaintsGetted ) {
            ///TODO refrech the list of paints

            if ( evt.error ) {
                alert( "error: loading the paintings data!!:" + evt.error );
            } else {
                //alert( "paintings data Loaded!!:" + evt.value );
                var li: any;
                evt.value.forEach(
                    function ( key, val ) {
                        li = $( '#listPaints' ).append( "<li id='" + key.Name + "'><h3>" + key.Name + "</h3></li>" );
                        li.on( 'click',
                            function () {
                                alert( 'item ' + key.Name + ' pressed' );
                            }
                        );
                    }
                    );
                $( 'listPaints' ).listview( "refresh" );
            }
        });

    gApp.PubSub.subscribe(new cmdJumpToPage_Paints, function (cmdJumpToPage_Paints) {
            $.mobile.changePage("index.html#pagePaints", {
                transition: "pop"
            })
        });

    gApp.PubSub.subscribe(new cmdJumpToPage_AboutMe, function (cmdJumpToPage_AboutMe) {
            $.mobile.changePage("index.html#pageMe", {
                transition: "pop"
            })
        });

    gApp.PubSub.subscribe(new cmdLoadBiography, function (cmdLoadBiography) {
        var bio: models.aboutMes.AboutMes;
        bio = new models.aboutMes.AboutMes();
        bio.get();
    });

    gApp.PubSub.subscribe(new models.aboutMes.evtBiographyGetted(null, null, null), function (evt:models.aboutMes.evtBiographyGetted) {
        $('#biography').html(evt.value.Biographie);
        //$('#biography').listview("refresh");
    });

    gApp.PubSub.publish(new cmdLoadPaints());
    gApp.PubSub.publish(new cmdLoadBiography());

}