export interface IExchangeLatestDetails {
    from: string;
    to: string;
    ammount: number;
}

export interface IExchangeHistoricalDetails {
    from: string;
    to: string;
    ammount: number;
    date: Date;
}