import { Client } from "./client";
import { Freelancer } from "./freelancer";

export interface Review {
    Id: number;
    Rating: number;
    Comment: string;
    FreelancerId: number;
    Freelancer:Freelancer
    ClientId: number;
    Client:Client;
  }
  