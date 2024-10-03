export interface Review {
    id: number;
    rating: number;
    comment: string;
    dateCreated: Date;
    freelancerId?: number;
    clientId?: number;
  }
  