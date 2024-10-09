import { Freelancer } from './freelancer';
import { Project } from './project';

export interface Bid {
  Id: number;
  ProjectId: number;
  Project: Project;
  FreelancerId: number;
  Freelancer: Freelancer;
  Amount: number;
  Proposal: string;
}
