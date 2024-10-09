import { Profile } from "./profile";

export interface PortfolioItem {
    Id: number;
    Title: string;
    Description: string;
    ImageUrl: string;
    ProfileId: number;
    Profile : Profile;
  }
  