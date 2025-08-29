import { Freelancer } from './freelancer';
import { Client } from './client';
import { Skill } from './skill';
import { Bid } from './bid';

export interface Project {
    id: number;
    title: string;
    description: string;
    clientId: number;
    client: Client;
    selectedFreelancerId?: number;
    selectedFreelancer?: Freelancer;
    requiredSkills: Skill[];
    budget: number;
    status: string;
    bids: Bid[];
    bidCount?: number; 
    projectDate:number;
}
