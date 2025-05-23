import { Freelancer } from './freelancer';
import { Project } from './project';

export interface Bid {
    id: number;
    projectId: number;
    project: Project;
    projectName?:string ;
    freelancerId: number;
    freelancer: Freelancer;
    amount: number;
    proposal: string;
}
