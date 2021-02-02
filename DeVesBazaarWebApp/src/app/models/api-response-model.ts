
export interface IResponse<T> {
    error?: IError;
    data?: T;
}

export interface IError {
    messageCode: string;
}

