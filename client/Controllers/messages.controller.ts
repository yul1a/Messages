module app.messages {
    import IMessage = app.models.IMessage;
    interface IMessagesController {
        CreateNewMessage(messageToAdd: app.models.IMessage): void;
        GetListOfMessages(): void;
        GetMessageBody(message: IMessage): void;
        DeleteMessage(message: IMessage): void;
        UpdateMessage(messageToUpdate: app.models.IMessage);
    }

    class MessagesController implements IMessagesController {
        private actionUrl: string;

        messages: app.models.IMessage[];
        newMessage: app.models.IMessage;
        body: string;

        static $inject = ["$http", "appSettings"];

        constructor(
            private $http: ng.IHttpService,
            private appSettings: any) {

            this.actionUrl = this.appSettings.serverPath + "api/messages/";
            this.messages = [];
            this.GetListOfMessages();
        }

        CreateNewMessage(messageToAdd: app.models.IMessage): void {
            this.$http.post(this.actionUrl, messageToAdd).success((data:IMessage) => {
                this.GetListOfMessages();
                this.newMessage.Header = "";
                this.newMessage.Body = "";
            });
            
        }

        GetListOfMessages(): void {
            this.$http.get(this.actionUrl )
                .success((data: app.models.IMessage[]) => {
                this.messages = data
            });
        }

        GetMessageBody(message: IMessage): void {
            this.$http.get(this.actionUrl + "body/" + message.Id).success((body:string) => 
                message.Body = body);
        }

        DeleteMessage(message: IMessage): void {
            this.$http.delete(this.actionUrl + message.Id).success(() => {
                this.GetListOfMessages();
            });
        }

        UpdateMessage(messageToUpdate: app.models.IMessage) {
            this.$http.patch(this.actionUrl + messageToUpdate.Id, messageToUpdate)
                .success((data:IMessage) => {
                    delete this.newMessage;
                    this.messages.forEach((message, index)=>{
                        if(message.Id === data.Id){
                            this.messages[index] = data;
                        }
                    })
                });
            
        }
    }

    angular.module("messagesModule")
        .controller("messages.controllers.messagesController",
            MessagesController);
}