module app.models {
 export interface IMessage {
  Id: number;
  Header: string;
  Body: string;
 }

 export class Message implements IMessage {

  constructor(
   public Id: number,
   public Header: string,
   public Body: string
  ) {

  }
 }
}
