export class ShortUrl {
    id: number;
    shortenedUrl: string;
    originalUrl: string;
    createdBy: number;

    constructor(id: number, shortenedUrl: string, originalUrl: string, createdBy: number) {
        this.id = id;
        this.shortenedUrl = shortenedUrl;
        this.originalUrl = originalUrl;
        this.createdBy = createdBy;
    }
}