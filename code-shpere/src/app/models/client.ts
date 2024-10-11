import { Project } from "./project";
import { Review } from "./review";

export interface Client {
    id: number;
    name: string;
    userName: string;
    email: string;
    passwordHash: string;
    role: string;
    rating: number;
    companyName: string;
    contactNumber: string;
    reviewsGiven: Review[];
    postedProjects: Project[];
}
