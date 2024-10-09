import { Review } from "../review";
import { Skill } from "../skill";
import { ProfileForCreate } from "./profile-for-create";

export interface FreelancerForCreate {
    Profile : ProfileForCreate;
    Name: string;
    Rating : number;
    UserName : string;
    Role : string;
    PasswordHash : string;
    Email : string;
    HourlyRate: number;
  }
  