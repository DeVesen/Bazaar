import { IResponse, IError } from "src/app/models/api-response-model";

export class GlobalHelper {

    public static createErrorResponse<T>(messageCode: string): IResponse<T> {
      return {
        error: this.createError(messageCode)
      };
    }
  
    public static createError(messageCode: string): IError {
      return {
        messageCode: messageCode
      };
    }
  
    public static sleep(ms: number): Promise<void> {
      return new Promise(resolve => setTimeout(resolve, ms));
    }

}
