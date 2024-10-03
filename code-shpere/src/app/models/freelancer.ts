import { Review } from "./review";
import { Skill } from "./skill";

export interface Freelancer {
    id: number;
    name: string;
    title: string;
    bio: string;
    hourlyRate: number;
    skills: Skill[];
    reviews: Review[];
  }
  