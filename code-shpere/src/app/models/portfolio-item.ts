import { Profile } from "./profile";

export interface PortfolioItem {
    id: number;
    title: string;
    description: string;
    imageUrl: string;
    profileId: number;
    profile: Profile;
}
