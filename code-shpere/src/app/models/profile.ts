import { PortfolioItem } from './portfolio-item';

export interface Profile {
  id: number;
  freelancerId: number;
  bio: string;
  portfolioItems: PortfolioItem[];
}
