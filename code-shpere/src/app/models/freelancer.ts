import { Bid } from "./bid";
import { Profile } from "./profile";
import { Project } from "./project";
import { Review } from "./review";

export interface Freelancer {
    id: number;
    name: string;
    rating: number;
    userName: string;
    role: string;
    passwordHash: string;
    email: string;
    bids: Bid[];
    reviewsReceived: Review[];
    completedProjects: Project[];
    profileId: number;
    profile: Profile;
}
