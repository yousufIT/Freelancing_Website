import { Freelancer } from './freelancer';
import { PortfolioItem } from './portfolio-item';
import { Skill } from './skill';

export interface Profile {
  Id: number;
  FreelancerId: number;
  Freelancer : Freelancer;
  Skills : Skill[];
  Portfolio : PortfolioItem[];
  Bio: string;
  PortfolioItems: PortfolioItem[];
}
