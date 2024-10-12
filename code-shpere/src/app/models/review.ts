import { Freelancer } from "./freelancer";
import { Client } from "./client";

export interface Review {
    id: number;
    rating: number;
    comment: string;
    freelancerId: number;
    freelancer: Freelancer;
    freelancerName?:string
    clientId: number;
    client: Client;
    clientName?:string;
}
