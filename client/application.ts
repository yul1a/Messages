module app {
 var main = angular.module("MessagesApp",
   ["messagesModule"])
  .constant("appSettings", {
   serverPath: "http://localhost:5001/"
  });
}