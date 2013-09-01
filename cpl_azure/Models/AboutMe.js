var models;
(function (models) {
    (function (aboutMes) {
        var evtBiographyGetted = (function () {
            function evtBiographyGetted(status, value, error) {
                this.status = status;
                this.value = value;
                this.error = error;
            }
            return evtBiographyGetted;
        })();
        aboutMes.evtBiographyGetted = evtBiographyGetted;

        var AboutMes = (function () {
            function AboutMes() {
                this._root = "api/AboutMeAPI";
            }
            AboutMes.prototype.get = function () {
                var JQryAjxSetting = {
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
                    jsonpCallback: 'itDoesntMatterNotAFunction'
                };
                $.ajax(JQryAjxSetting);
            };
            return AboutMes;
        })();
        aboutMes.AboutMes = AboutMes;
    })(models.aboutMes || (models.aboutMes = {}));
    var aboutMes = models.aboutMes;
})(models || (models = {}));
//# sourceMappingURL=aboutMe.js.map
