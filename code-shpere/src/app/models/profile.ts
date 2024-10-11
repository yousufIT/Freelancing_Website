import { Freelancer } from './freelancer';
import { PortfolioItem } from './portfolio-item';
import { Skill } from './skill';

export interface Profile {
    id: number;
    freelancerId: number;
    freelancer: Freelancer;
    skills: Skill[];
    portfolio: PortfolioItem[];
    bio: string;
    portfolioItems: PortfolioItem[];
}
