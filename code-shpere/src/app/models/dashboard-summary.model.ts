import { RecentProject } from './recent-project.model';
import { TopFreelancer } from './top-freelancer.model';
import { RecentBid } from './recent-bid.model';

export interface DashboardSummary {
  totalClients: number;
  totalFreelancers: number;
  totalProjects: number;
  totalBids: number;
  totalReviews: number;
  totalSkills: number;
  recentProjects: RecentProject[];
  topFreelancers: TopFreelancer[];
  recentBids: RecentBid[];
}
