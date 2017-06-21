module app.home {
    var main = angular.module("messagesModule");

    main.config(routeConfig);

    routeConfig.$inject = ["$routeProvider"];
    function routeConfig($routeProvider: ng.route.IRouteProvider): void {
        $routeProvider.when("/",
                {
                    templateUrl: "client/Templates/messagesTemplate.html",
                    controller: "messages.controllers.messagesController as vm"
                })
            .otherwise("/");
    }
}