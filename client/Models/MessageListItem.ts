module app.models {
 export interface IMessageListItem {
  Id: number;
  Header: string;
 }

 export class MessageListItem implements IMessageListItem {

  constructor(
   public Id: number,
   public Header: string
  ) {

  }
 }
}
