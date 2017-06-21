var app;
(function (app) {
    var models;
    (function (models) {
        var MessageListItem = (function () {
            function MessageListItem(Id, Header) {
                this.Id = Id;
                this.Header = Header;
            }
            return MessageListItem;
        })();
        models.MessageListItem = MessageListItem;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=messageListItem.js.map