import { Freelancer } from "./freelancer";
import { Client } from "./client";

export interface Review {
    id: number;
    rating: number;
    comment: string;
    freelancerId: number;
    freelancer: Freelancer;
    clientId: number;
    client: Client;
}
