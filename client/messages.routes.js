var app;
(function (app) {
    var home;
    (function (home) {
        var main = angular.module("messagesModule");
        main.config(routeConfig);
        routeConfig.$inject = ["$routeProvider"];
        function routeConfig($routeProvider) {
            $routeProvider.when("/", {
                templateUrl: "client/Templates/messagesTemplate.html",
                controller: "messages.controllers.messagesController as vm"
            })
                .otherwise("/");
        }
    })(home = app.home || (app.home = {}));
})(app || (app = {}));
//# sourceMappingURL=messages.routes.js.map