var app;
(function (app) {
    var models;
    (function (models) {
        var Message = (function () {
            function Message(Id, Header, Body) {
                this.Id = Id;
                this.Header = Header;
                this.Body = Body;
            }
            return Message;
        })();
        models.Message = Message;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=message.js.map