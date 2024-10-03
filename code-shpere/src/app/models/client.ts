import { Review } from "./review";

export interface Client {
    id: number;
    name: string;
    companyName: string;
    bio: string;
    reviews: Review[];
  }
  