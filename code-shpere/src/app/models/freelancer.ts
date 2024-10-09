import { Bid } from "./bid";
import { Profile } from "./profile";
import { Project } from "./project";
import { Review } from "./review";

export interface Freelancer {
    Id: number;
    Name: string;
    Rating : number;
    UserName : string;
    Role : string;
    PasswordHash : string;
    Email : string;
    HourlyRate: number;
    Bids: Bid[];
    ReviewsReceived: Review[];
    CompletedProjects : Project[];
    ProfileId : number;
    Profile : Profile;
  }
  