var app;
(function (app) {
    var messages;
    (function (messages) {
        var MessagesController = (function () {
            function MessagesController($http, appSettings) {
                this.$http = $http;
                this.appSettings = appSettings;
                this.actionUrl = this.appSettings.serverPath + "api/messages/";
                this.messages = [];
                this.GetListOfMessages();
            }
            MessagesController.prototype.CreateNewMessage = function (messageToAdd) {
                var _this = this;
                this.$http.post(this.actionUrl, messageToAdd).success(function (data) {
                    _this.GetListOfMessages();
                    _this.newMessage.Header = "";
                    _this.newMessage.Body = "";
                });
            };
            MessagesController.prototype.GetListOfMessages = function () {
                var _this = this;
                this.$http.get(this.actionUrl)
                    .success(function (data) {
                    _this.messages = data;
                });
            };
            MessagesController.prototype.GetMessageBody = function (message) {
                this.$http.get(this.actionUrl + "body/" + message.Id).success(function (body) {
                    return message.Body = body;
                });
            };
            MessagesController.prototype.DeleteMessage = function (message) {
                var _this = this;
                this.$http.delete(this.actionUrl + message.Id).success(function () {
                    _this.GetListOfMessages();
                });
            };
            MessagesController.prototype.UpdateMessage = function (messageToUpdate) {
                var _this = this;
                this.$http.patch(this.actionUrl + messageToUpdate.Id, messageToUpdate)
                    .success(function (data) {
                    delete _this.newMessage;
                    _this.messages.forEach(function (message, index) {
                        if (message.Id === data.Id) {
                            _this.messages[index] = data;
                        }
                    });
                });
            };
            MessagesController.$inject = ["$http", "appSettings"];
            return MessagesController;
        })();
        angular.module("messagesModule")
            .controller("messages.controllers.messagesController", MessagesController);
    })(messages = app.messages || (app.messages = {}));
})(app || (app = {}));
//# sourceMappingURL=messages.controller.js.map