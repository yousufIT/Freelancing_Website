import { ProfileForCreate } from "./profile-for-create";

export interface FreelancerForCreate {
    profile: ProfileForCreate;
    name: string;
    rating: number;
    userName: string;
    role: string;
    passwordHash: string;
    email: string;
}
