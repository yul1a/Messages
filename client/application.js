var app;
(function (app) {
    var main = angular.module("MessagesApp", ["messagesModule"])
        .constant("appSettings", {
        serverPath: "http://localhost:5001/"
    });
})(app || (app = {}));
//# sourceMappingURL=application.js.map